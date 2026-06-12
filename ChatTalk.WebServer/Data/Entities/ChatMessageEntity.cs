namespace ChatTalk.WebServer.Data.Entities
{
    public class ChatMessageEntity
    {
        public long Id { get; }

        public string MessageId { get; set; } = string.Empty;

        public string SenderName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreateAt { get; }
    }
}