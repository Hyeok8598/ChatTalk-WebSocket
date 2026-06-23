using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Constant
{
    public static class MessageType
    {
        public const string Msg = "MSG";
        public const string Whisper = "WHISPER";
        public const string UserList = "USRLIST";
        public const string Join = "JOIN";
        public const string Leave = "LEAVE";
        public const string System = "SYSTEM";
        public const string File = "FILE";
    }
}
