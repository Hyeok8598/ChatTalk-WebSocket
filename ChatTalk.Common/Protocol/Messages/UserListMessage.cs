using ChatTalk.Common.Protocol.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class UserListMessage : BaseMessage
    {
        public string[] Users {  get; set; } = Array.Empty<string>();

        public UserListMessage()
        {
            Type = MessageType.UserList;
        }
    }
}
