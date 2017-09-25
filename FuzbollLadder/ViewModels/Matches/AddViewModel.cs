using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels.Matches
{
    public class AddViewModel
    {
        [Required]
        public string WinnerNamesCsv { get; set; }

        [Required]
        public string LoserNamesCsv { get; set; }
    }
}