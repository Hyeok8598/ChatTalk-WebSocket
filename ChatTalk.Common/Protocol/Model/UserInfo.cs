using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Model
{
    public class UserInfo
    {
        public string UserId { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;

        public void SetUserId(string userId)
        {
            UserId = userId;
        }

        public void SetUserName(string userName)
        {
            UserName = userName;
        }
}}
