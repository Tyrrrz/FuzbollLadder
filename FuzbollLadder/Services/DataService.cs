using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FuzbollLadder.Models;
using FuzbollLadder.Options;
using LiteDB;
using Microsoft.Extensions.Options;
using Tyrrrz.Extensions;

namespace FuzbollLadder.Services
{
    public class DataService : IDataService, IDisposable
    {
        private readonly IRatingService _ratingService;
        private readonly LiteDatabase _db;

        public DataService(IOptions<DatabaseOptions> optionsAccessor, IRatingService ratingService)
        {
            var options = optionsAccessor.Value;
            _ratingService = ratingService;

            // Configure DB and mapping
            _db = new LiteDatabase(options.ConnectionString);
            _db.Mapper.Entity<Player>().Id(p => p.Id);
            _db.Mapper.Entity<Player>().Ignore(p => p.TotalGames);
            _db.Mapper.Entity<Player>().Ignore(p => p.WinRate);
            _db.Mapper.Entity<Match>().Id(m => m.Id);
            _db.Mapper.Entity<Match>().DbRef(m => m.Winners);
            _db.Mapper.Entity<Match>().DbRef(m => m.Losers);

            // Create directory for DB if needed
            var dbFilePath = options.ConnectionString.Contains("Filename=")
                ? options.ConnectionString.SubstringAfter("Filename=").SubstringUntil(";")
                : options.ConnectionString;
            var dbDirPath = Path.GetDirectoryName(dbFilePath);
            Directory.CreateDirectory(dbDirPath);
        }

        private void ProcessMatch(Match match)
        {
            // Get average ratings
            var avgWinnerRating = match.Winners.Average(p => p.Rating);
            var avgLoserRating = match.Losers.Average(p => p.Rating);

            // Calculate rating delta based on result
            var ratingDelta = _ratingService.CalculateDelta(avgWinnerRating, avgLoserRating);
            match.RatingDelta = ratingDelta;

            // Update player stats
            foreach (var player in match.Winners)
            {
                player.Wins++;
                player.Rating += ratingDelta;
            }
            foreach (var player in match.Losers)
            {
                player.Losses++;
                player.Rating -= ratingDelta;
            }

            // Save
            _db.GetCollection<Player>().Update(match.Winners);
            _db.GetCollection<Player>().Update(match.Losers);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _db.GetCollection<Player>().FindAll();
        }

        public Player GetPlayer(int id)
        {
            return _db.GetCollection<Player>().FindById(id);
        }

        public Player AddPlayer(string name)
        {
            var player = new Player
            {
                Name = name,
                Rating = _ratingService.DefaultRating
            };
            var newPlayer =_db.GetCollection<Player>().Insert(player);
            return player;
        }

        public IEnumerable<Match> GetAllMatches()
        {
            return _db.GetCollection<Match>()
                .Include(m => m.Winners)
                .Include(m => m.Losers)
                .FindAll();
        }

        public Match GetMatch(int id)
        {
            return _db.GetCollection<Match>().FindById(id);
        }

        public void AddMatch(DateTime date, IReadOnlyList<Player> winners, IReadOnlyList<Player> losers)
        {
            // Add match
            var match = new Match
            {
                Date = date,
                Winners = winners.OrderBy(p => p.Id).ToArray(),
                Losers = losers.OrderBy(p => p.Id).ToArray()
            };

            // Process match and update player stats
            ProcessMatch(match);

            // Add match
            _db.GetCollection<Match>().Insert(match);
        }

        public void DeleteMatch(int id)
        {
            _db.GetCollection<Match>().Delete(id);
        }

        public void RecalculateMatches()
        {
            // Reset stats
            foreach (var player in GetAllPlayers())
            {
                player.Wins = 0;
                player.Losses = 0;
                player.Rating = _ratingService.DefaultRating;
                _db.GetCollection<Player>().Update(player);
            }

            // Process all existing matches
            foreach (var match in GetAllMatches())
            {
                ProcessMatch(match);
            }
        }

        public IEnumerable<PlayerStat> GetAllPlayerStats()
        {
            // Get matches within 30 days
            var today = DateTime.Today;
            var weekAgo = today.AddDays(-7);
            var monthAgo = today.AddDays(-30);
            var matchesToday = GetAllMatches().Where(m => m.Date >= monthAgo).ToArray();

            // Get all players
            var players = GetAllPlayers().ToArray();

            // Yield the stats
            foreach (var player in players)
            {
                // Calculate deltas
                var dailyRatingDelta = 0d;
                var weeklyRatingDelta = 0d;
                var monthlyRatingDelta = 0d;
                foreach (var match in matchesToday)
                {
                    // Multiplier
                    var mult = 0;
                    if (match.Winners.Any(p => p.Id == player.Id))
                        mult = 1;
                    else if (match.Losers.Any(p => p.Id == player.Id))
                        mult = -1;

                    // Add values
                    if (match.Date >= today)
                        dailyRatingDelta += mult * match.RatingDelta;
                    if (match.Date >= weekAgo)
                        weeklyRatingDelta += mult * match.RatingDelta;
                    if (match.Date >= monthAgo)
                        monthlyRatingDelta += mult * match.RatingDelta;
                }

                var stat = new PlayerStat
                {
                    Player = player,
                    DailyRatingDelta = dailyRatingDelta,
                    WeeklyRatingDelta = weeklyRatingDelta,
                    MonthlyRatingDelta = monthlyRatingDelta
                };

                yield return stat;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
