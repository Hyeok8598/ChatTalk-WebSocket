namespace ChatTalk.WebServer.Data.Dapper.Entities
{
    public class UsersEntity
    {
        public long Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
