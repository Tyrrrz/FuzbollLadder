using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuzbollLadder.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        [NotMapped]
        public int TotalGames => Wins + Losses;

        [NotMapped]
        public double WinRate => TotalGames == 0 ? 0 : 1d * Wins / TotalGames;

        public double Rating { get; set; } = 1200;
    }
}