using System;

namespace FuzbollLadder.Models
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Player[] Winners { get; set; }

        public Player[] Losers { get; set; }

        public double RatingDelta { get; set; }
    }
}