using System;
using System.Collections.Generic;
using FuzbollLadder.Models;

namespace FuzbollLadder.Services
{
    public interface IDataService
    {
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayer(int id);
        void AddPlayer(string name);

        IEnumerable<Match> GetAllMatches();
        Match GetMatch(int id);
        void AddMatch(DateTime date, IReadOnlyList<Player> winners, IReadOnlyList<Player> losers);
        void DeleteMatch(int id);
        void RecalculateMatches();
    }
}