using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public abstract class RoleBase
    {
        public Guid RoleId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
