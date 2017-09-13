using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels
{
    public class SubmitMatchViewModel
    {
        [Required]
        public string WinnerNamesCsv { get; set; }

        [Required]
        public string LoserNamesCsv { get; set; }
    }
}