using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class ChatMessage : BaseMessage
    {
        public string Sender { get; set; } = string.Empty;
        public string MessageId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public ChatMessage() 
        {
            Type = "MSG";
        }
    }
}
