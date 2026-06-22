using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Model;
using ChatTalk.Common.Protocol.Serialization;
using System.Net.WebSockets;
using System.Text;

namespace ChatTalk.WebServer.Network
{
    public class WebSocketHandler
    {
        private readonly WebSocket _webSocket;
        
        private string ConnecedtId { get; } = Guid.NewGuid().ToString();
        public UserInfo UserInfo { get; } = new();

        public WebSocketHandler(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public async Task SendAsync(BaseMessage baseMessage)
        {
            string sendJson = MessageSerializer.Serialize(baseMessage);
            byte[] sendBytes = Encoding.UTF8.GetBytes(sendJson);
            await _webSocket.SendAsync(new ArraySegment<byte>(sendBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task CloseAsync()
        {
            if(_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Closed",
                    CancellationToken.None
                );
            }
        }

        public string GetConnectId()
        {
            return ConnecedtId;
        }

        public WebSocket GetWebSocket()
        {
            return _webSocket;
        }
    }
}
