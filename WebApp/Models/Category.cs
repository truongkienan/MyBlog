using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("Category")]
    public class Category
    {
        public byte CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "Use letters only please")]
        public string CategoryUrl { get; set; }
        public bool ShowDefault { get; set; }
        public byte Position { get; set; }
        public bool Active { get; set; }
    }
}
