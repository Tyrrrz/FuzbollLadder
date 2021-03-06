﻿using System;
using System.Collections.Generic;
using FuzbollLadder.Models;

namespace FuzbollLadder.Services
{
    public interface IDataService
    {
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayer(int id);
        Player AddPlayer(string name);

        IEnumerable<Match> GetAllMatches();
        Match GetMatch(int id);
        Match AddMatch(DateTime date, IEnumerable<Player> winners, IEnumerable<Player> losers);
        void DeleteMatch(int id);
        void RecalculateMatches();

        IEnumerable<PlayerStat> GetAllPlayerStats();
    }
}