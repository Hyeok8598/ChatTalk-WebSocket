using ChatTalk.Common.Protocol.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class SystemMessage : BaseMessage
    {
        public string content { get; set; } = string.Empty;
        public SystemMessage()
        {
            Type = MessageType.System;
        }
    }
}
