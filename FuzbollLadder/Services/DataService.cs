using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzbollLadder.Data;
using FuzbollLadder.Models;
using Microsoft.EntityFrameworkCore;

namespace FuzbollLadder.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;
        private readonly IRatingService _ratingService;

        public DataService(DataContext context, IRatingService ratingService)
        {
            _context = context;
            _ratingService = ratingService;
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
                _context.Entry(player).State = EntityState.Modified;
            }
            foreach (var player in match.Losers)
            {
                player.Losses++;
                player.Rating += ratingDelta.LoserDelta;
                _context.Entry(player).State = EntityState.Modified;
            }
        }

        public async Task<IReadOnlyList<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.ToArrayAsync();
        }

        public async Task<Player> GetPlayerAsync(int id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPlayerAsync(string name)
        {
            var player = new Player {Name = name};
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Match>> GetAllMatchesAsync()
        {
            return await _context.Matches.ToArrayAsync();
        }

        public async Task<Match> GetMatchAsync(int id)
        {
            return await _context.Matches.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddMatchAsync(DateTime date, IReadOnlyList<Player> winners, IReadOnlyList<Player> losers)
        {
            // Add match
            var match = new Match
            {
                Date = date,
                Winners = winners,
                Losers = losers
            };
            await _context.Matches.AddAsync(match);

            // Update player stats
            ProcessMatch(match);

            await _context.SaveChangesAsync();
        }

        public async Task RecalculateMatchesAsync()
        {
            // Reset stats
            foreach (var player in _context.Players)
            {
                player.Wins = 0;
                player.Losses = 0;
                player.Rating = _ratingService.DefaultRating;
                _context.Entry(player).State = EntityState.Modified;
            }

            // Process all existing matches
            foreach (var match in _context.Matches)
            {
                ProcessMatch(match);
            }

            await _context.SaveChangesAsync();
        }
    }
}
