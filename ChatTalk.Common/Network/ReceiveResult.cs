using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Network
{
    public class ReceiveResult
    {
        public required WebSocketReceiveResult Result { get; init; }
        public string Json { get; init; } = string.Empty;
    }
}