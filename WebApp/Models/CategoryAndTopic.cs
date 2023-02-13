namespace WebApp.Models
{
    public class CategoryAndTopic
    {
        public byte CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public short TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicUrl { get; set; }
        public string Content { get; set; }
    }
}
