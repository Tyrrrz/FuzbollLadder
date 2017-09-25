using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels.Matches
{
    public class AddMatchViewModel
    {
        [Required]
        public string WinnerNamesCsv { get; set; }

        [Required]
        public string LoserNamesCsv { get; set; }
    }
}