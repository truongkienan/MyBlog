using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("Topic")]
    public class Topic
    {
        static string[] arr = { "One", "Two", "Three", "Four" };
        byte columnIndex;
        public short TopicId { get; set; }
        [Display(Name = "Topic Name")]
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3)]
        public string TopicName { get; set; }
        public string TopicUrl { get; set; }
        [Display(Name = "Content")]
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3)]
        public string Content { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public byte ColumnIndex { get { return columnIndex; } set { columnIndex = value; } }
        public short Position { get; set; }
        public string ColumnName {
            get
            {
                return arr[columnIndex];
            } 
        }
        public bool Active { get; set; }

    }
}
