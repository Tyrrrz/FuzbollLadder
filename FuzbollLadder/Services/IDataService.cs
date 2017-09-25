using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuzbollLadder.Models;

namespace FuzbollLadder.Services
{
    public interface IDataService
    {
        Task<IReadOnlyList<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerAsync(int id);
        Task AddPlayerAsync(string name);

        Task<IReadOnlyList<Match>> GetAllMatchesAsync();
        Task<Match> GetMatchAsync(int id);
        Task AddMatchAsync(DateTime date, IReadOnlyList<Player> winners, IReadOnlyList<Player> losers);
        Task RecalculateMatchesAsync();
    }
}