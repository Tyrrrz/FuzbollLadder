using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels.Matches
{
    public class AddViewModel
    {
        public string winnerNames { get; set; }
        public string loserNames { get; set; }
        [Required]
        public string WinnerName1 { get; set; }
        public string WinnerName2{ get; set; }

        [Required]
        public string LoserName1 { get; set; }
        public string LoserName2 { get; set; }
    }
}