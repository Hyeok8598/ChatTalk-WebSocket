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
        public string Sender {  get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public WhisperMessage()
        {
            Type = MessageType.Whisper;
        }
    }
}
