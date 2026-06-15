using ChatTalk.Common.Network;
using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Model;
using ChatTalk.Common.Protocol.Serialization;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net.WebSockets;
using System.Text;

namespace ChatTalk.WebServer.Network
{
    public class WebSocketHandler
    {
        private readonly WebSocketServer _server;
        private readonly WebSocket _webSocket;
        
        public string ConnectId { get; } = Guid.NewGuid().ToString();
        public UserInfo UserInfo { get; private set; } = new();

        public WebSocketHandler(WebSocketServer server, WebSocket webSocket)
        {
            _server = server;
            _webSocket = webSocket;
        }

        public async Task RunAsync()
        {
            var receive = await ReceiveAsync();
            try
            {
                if (receive.Result.MessageType == WebSocketMessageType.Close)
                {
                    //await CloseAsync(receive);
                    _server.Clients.Remove(ConnectId);
                    await BroadcastAsync(new UserListMessage
                    {
                        
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
                        UsersDto usersDto = new UsersDto { UserId = joinMessage.UserId };
                        UsersEntity? user = await _server.UsersService.SelectOne001(usersDto);

                        if (user == null)
                        {
                            Console.WriteLine("유저아이디가 없음");
                            return;
                        }

                        UserInfo.UserId = user.UserId;
                        UserInfo.UserName = user.UserName;

                        _server.Clients.Add(ConnectId, this);

                        await BroadcastAsync(new UserListMessage
                        {
                            Users = _server.Clients.GetUserInfo()
                        });

                        return;
                    }

                    if (baseMessage is LeaveMessage leaveMessage)
                    {
                        _server.Clients.Remove(ConnectId);
                        await BroadcastAsync(new UserListMessage
                        {
                            Users = _server.Clients.GetUserInfo()
                        });

                        return;
                    }

                    if (baseMessage is ChatMessage chatMessage)
                    {
                        ChatMessageDto chatMessageDto = new ChatMessageDto
                        {
                            SenderName = chatMessage.Sender,
                            MessageId = chatMessage.MessageId,
                            Content = chatMessage.Content
                        };

                        await _server.ChatMessageService.Insert001(chatMessageDto);
                    }
                    await BroadcastAsync(baseMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"메시지 핸들 중 오류 {ex.Message}");
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
    }
}
