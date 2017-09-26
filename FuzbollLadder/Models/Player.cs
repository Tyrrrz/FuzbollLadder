namespace FuzbollLadder.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int TotalGames => Wins + Losses;

        public double WinRate => TotalGames == 0 ? 0 : 1d * Wins / TotalGames;

        public double Rating { get; set; }

        public Match[] Matches { get; set; }
    }
}