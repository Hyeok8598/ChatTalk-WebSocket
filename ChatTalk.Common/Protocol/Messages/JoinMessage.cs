using ChatTalk.Common.Protocol.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class JoinMessage : BaseMessage
    {
        public string UserId { get; set; } = string.Empty;

        public JoinMessage()
        {
            Type = MessageType.Join;
        }
    }
}
