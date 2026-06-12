namespace ChatTalk.WebServer.Data.Dapper.Dto
{
    public class ChatMessageDto
    {
        public long Id { get; set; }

        public string MessageId { get; set; } = string.Empty;

        public string SenderName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreateAt { get; }
    }
}
