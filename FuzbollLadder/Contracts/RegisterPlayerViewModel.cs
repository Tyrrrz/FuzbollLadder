using System.ComponentModel.DataAnnotations;

namespace FuzbollLadder.Contracts
{
    public class RegisterPlayerViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}