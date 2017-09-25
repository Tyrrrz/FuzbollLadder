using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels.Players
{
    public class AddPlayerViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}