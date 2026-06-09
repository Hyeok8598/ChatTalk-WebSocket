namespace ChatTalk.WebServer.Data.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string LoginId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
