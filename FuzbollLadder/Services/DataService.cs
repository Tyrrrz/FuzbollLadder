using System;
using System.Collections.Generic;
using System.Linq;
using FuzbollLadder.Models;
using FuzbollLadder.Options;
using LiteDB;
using Microsoft.Extensions.Options;

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

            _db = new LiteDatabase(options.ConnectionString);
            _db.Mapper.Entity<Player>().Id(p => p.Id);
            _db.Mapper.Entity<Player>().Ignore(p => p.TotalGames);
            _db.Mapper.Entity<Player>().Ignore(p => p.WinRate);
            _db.Mapper.Entity<Match>().Id(m => m.Id);
            _db.Mapper.Entity<Match>().DbRef(m => m.Winners);
            _db.Mapper.Entity<Match>().DbRef(m => m.Losers);
        }

        private void ProcessMatch(Match match)
        {
            // Get average ratings
            var avgWinnerRating = match.Winners.Average(p => p.Rating);
            var avgLoserRating = match.Losers.Average(p => p.Rating);

            // Calculate rating delta based on result
            var ratingDelta = _ratingService.CalculateDelta(avgWinnerRating, avgLoserRating);

            // Update player stats
            foreach (var player in match.Winners)
            {
                player.Wins++;
                player.Rating += ratingDelta.WinnerDelta;
            }
            foreach (var player in match.Losers)
            {
                player.Losses++;
                player.Rating += ratingDelta.LoserDelta;
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

        public void AddPlayer(string name)
        {
            var player = new Player
            {
                Name = name,
                Rating = _ratingService.DefaultRating
            };
            _db.GetCollection<Player>().Insert(player);
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
            _db.GetCollection<Match>().Insert(match);

            // Update player stats
            ProcessMatch(match);
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

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
