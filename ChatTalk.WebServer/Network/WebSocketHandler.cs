using ChatTalk.Common.Network;
using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Serialization;
using System.Net.WebSockets;
using System.Text;

namespace ChatTalk.WebServer.Network
{
    public class WebSocketHandler
    {
        private readonly WebSocketServer _server;
        private readonly WebSocket _webSocket;
        private string userName = string.Empty;
        private string userId = string.Empty;

        public WebSocketHandler(WebSocketServer server, WebSocket webSocket)
        {
            _server = server;
            _webSocket = webSocket;
        }

        public async Task RunAsync()
        {
            var receive = await ReceiveAsync();

            if (receive.Result.MessageType == WebSocketMessageType.Close)
            {
                //await CloseAsync(receive);
                _server.RemoveClient(userId);
                await BroadcastAsync(new UserListMessage
                {
                    Users = _server.GetUserNames()
                });
            }
            else if (receive.Result.MessageType == WebSocketMessageType.Text)
            {
                BaseMessage? baseMessage = CreateMessage(receive);

                if (baseMessage == null)
                {
                    return;
                }

                if (baseMessage is JoinMessage joinMessage)
                {
                    userId = Guid.NewGuid().ToString();
                    userName = joinMessage.UserName;

                    _server.AddClient(userId, this);

                    await BroadcastAsync(new UserListMessage
                    {
                        Users = _server.GetUserNames()
                    });

                    return;
                }

                if (baseMessage is LeaveMessage leaveMessage)
                {
                    _server.RemoveClient(leaveMessage.UserName);
                    await BroadcastAsync(new UserListMessage
                    {
                        Users = _server.GetUserNames()
                    });

                    return;
                }

                await BroadcastAsync(baseMessage);
            }
        }

        public async Task<ReceiveResult> ReceiveAsync()
        {
            var buffer = new byte[1024];
            var receive = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string json = Encoding.UTF8.GetString(buffer, 0, receive.Count);
            Console.WriteLine($"[Receive] Type: {receive.MessageType}, Size: {receive.Count}");

            return new ReceiveResult
            {
                Result = receive,
                Json = json
            };
        }

        //public async Task CloseAsync(ReceiveResult receive)
        //{
        //    /* WebSocketServer 책임이므로 추후 해당 코드는 변경되야함 */
        //    Console.WriteLine($"[Close] : {receive.Result.CloseStatus} - {receive.Result.CloseStatusDescription}");
        //    //await _webSocket.CloseAsync(receive.Result.CloseStatus ?? WebSocketCloseStatus.NormalClosure, receive.Result.CloseStatusDescription, CancellationToken.None);
        //}

        public async Task SendAsync(byte[] sendBytes)
        {
            await _webSocket.SendAsync(new ArraySegment<byte>(sendBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task BroadcastAsync(BaseMessage baseMessage)
        {   
            string sendJson = MessageSerializer.Serialize(baseMessage);
            Console.WriteLine($"[Send] : {sendJson}");
            byte[] sendBytes = Encoding.UTF8.GetBytes(sendJson);
            await _server.BroadCastAsync(sendBytes);
        }

        private BaseMessage? CreateMessage(ReceiveResult receive)
        {
            BaseMessage? message = MessageConverter.Create(receive.Json);

            if (message == null)
            {
                Console.WriteLine($"[Error] : Json is not correct.");
                return null;
            }

            return message;
        }

        public string GetUserName()
        {
            return userName;
        }
    }
}
