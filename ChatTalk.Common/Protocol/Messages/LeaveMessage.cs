using ChatTalk.Common.Protocol.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class LeaveMessage : BaseMessage
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public LeaveMessage()
        {
            Type = MessageType.Leave;
        }
    }
}
