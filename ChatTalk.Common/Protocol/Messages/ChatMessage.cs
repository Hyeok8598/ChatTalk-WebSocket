namespace ChatTalk.Common.Protocol.Messages
{
    public class ChatMessage : BaseMessage
    {
        public string MessageId { get; set; } = string.Empty;
        public string SenderUserId { get; set; } = string.Empty;
        public string Messagetype { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public ChatMessage() 
        {
            Type = "MSG";
        }
    }
}
