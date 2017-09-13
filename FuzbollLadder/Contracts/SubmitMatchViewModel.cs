using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.Contracts
{
    public class SubmitMatchViewModel
    {
        [Required]
        public string WinnerNamesCsv { get; set; }

        [Required]
        public string LoserNamesCsv { get; set; }
    }
}