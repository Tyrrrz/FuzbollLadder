using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels.Players
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}