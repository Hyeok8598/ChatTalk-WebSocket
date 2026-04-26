using System.Net.WebSockets;
using System;
using System.Text;
using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Parsing;

namespace ChatTalk.WebServer;

public class WebSocketServer
{
	public async Task HandleAsync(WebSocket webSocket)
	{
		var buffer = new byte[1024];

        Console.WriteLine("[Connected] WebSocket opened");
		try
		{
			while (webSocket.State == WebSocketState.Open)
			{
				var receive = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				Console.WriteLine($"[Receive] Type: {receive.MessageType}, Size: {receive.Count}");

				if (receive.MessageType == WebSocketMessageType.Close)
				{
					Console.WriteLine($"[Close] : {receive.CloseStatus} - {receive.CloseStatusDescription}");
					await webSocket.CloseAsync(receive.CloseStatus ?? WebSocketCloseStatus.NormalClosure, receive.CloseStatusDescription, CancellationToken.None);
					break;
				}

				if (receive.MessageType == WebSocketMessageType.Text)
				{
					var message = Encoding.UTF8.GetString(buffer, 0, receive.Count);
					Console.WriteLine($"[Message] : {message}");

                    ProtocolMessage prot = MessageParser.Parse(message);
                    switch (prot)
					{
						case ChatProtocolMessage chat:
                            Console.WriteLine($"[CHAT Message] : {chat.Content}");
                            break;
                        case WhisperProtocolMessage whisper:
                            Console.WriteLine($"[WHISPER Message] : {whisper.FromUserName} {whisper.ToUserName} {whisper.Content}");
                            break;
                        case UsrListProtocolMessage userlist:
                            Console.WriteLine($"[USERLIST Message] : {string.Join(", ", userlist.UserListContent)}");
							break;
                        case IUserStatusProtocolMessage userStatus:
                            Console.WriteLine($"[USERSTATUS Message] : {userStatus.UserName} {userStatus.StatusText}");
                            break;
                    }
					await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, receive.Count), receive.MessageType, receive.EndOfMessage, CancellationToken.None);
				}
			}
		}
		catch (Exception ex)
		{
            Console.WriteLine($"[Error] {ex.Message}");
        }

        Console.WriteLine("[Disconnected] WebSocket closed");
    }
}
