namespace ChatTalk.WebServer.Data.Dapper.Dto
{
    public class ChatMessageDto
    {
        public long Id { get; set; }
        public string MessageId { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public string SenderUserId { get; set; } = string.Empty;
        public string TargetUserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreateAt { get; } = DateTime.Now;
    }
}
