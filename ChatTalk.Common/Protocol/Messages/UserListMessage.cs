using ChatTalk.Common.Protocol.Constant;
using ChatTalk.Common.Protocol.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class UserListMessage : BaseMessage
    {
        public UserInfo[] Users {  get; set; } = Array.Empty<UserInfo>();

        public UserListMessage()
        {
            Type = MessageType.UserList;
        }
    }
}
