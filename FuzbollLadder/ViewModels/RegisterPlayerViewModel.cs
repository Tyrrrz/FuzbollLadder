using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.ViewModels
{
    public class RegisterPlayerViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}