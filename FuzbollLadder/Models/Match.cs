using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual IReadOnlyList<Player> Winners { get; set; }

        public virtual IReadOnlyList<Player> Losers { get; set; }
    }
}