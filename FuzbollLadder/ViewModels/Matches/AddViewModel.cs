using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels.Matches
{
    public class AddViewModel
    {
        [Required]
        public int []WinnerIds { get; set; }
        public int []LoserIds { get; set; }
    }
}