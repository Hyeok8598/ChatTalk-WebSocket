using ChatTalk.Common.Protocol.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class WhisperMessage : BaseMessage
    {
        public string MessageId { get; set; } = string.Empty;
        public string SenderUserId { get; set; } = string.Empty;
        public string SenderUserName { get; set; } = string.Empty;
        public string TargetUserId { get; set; } = string.Empty;
        public string TargetUserName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public WhisperMessage()
        {
            Type = MessageType.Whisper;
        }
    }
}
