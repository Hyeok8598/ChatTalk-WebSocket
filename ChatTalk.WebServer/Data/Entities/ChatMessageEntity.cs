using Microsoft.EntityFrameworkCore;

namespace ChatTalk.WebServer.Data.Entities
{
    public class ChatMessageEntity
    {
        public long Id { get; set; }
        public string MessageId { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public DateTime CreaeteAt { get; set; } = DateTime.Now;
    }
}
