using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ChangeModel
    {
        public string Username { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
