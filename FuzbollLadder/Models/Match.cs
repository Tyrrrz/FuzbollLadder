using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FuzbollLadder.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string WinnerIdsCsv { get; set; }

        [NotMapped]
        public IReadOnlyList<int> WinnerIds
        {
            get => WinnerIdsCsv.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            set => WinnerIdsCsv = string.Join(",", value);
        }

        public string LoserIdsCsv { get; set; }

        [NotMapped]
        public IReadOnlyList<int> LoserIds
        {
            get => LoserIdsCsv.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            set => LoserIdsCsv = string.Join(",", value);
        }
    }
}