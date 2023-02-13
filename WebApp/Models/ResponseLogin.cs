namespace WebApp.Models
{
    public class ResponseLogin
    {
        public Guid MemberId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
