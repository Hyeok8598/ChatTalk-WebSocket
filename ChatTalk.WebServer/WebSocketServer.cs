using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Serialization;
using System.Net.WebSockets;
using System.Text;

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
					string json = Encoding.UTF8.GetString(buffer, 0, receive.Count);
					BaseMessage? message = MessageConverter.Create(json);
					Console.WriteLine($"[Message] : {json}");

					if (message == null) continue;

					string sendJson  = MessageSerializer.Serialize(message);
					byte[] sendBytes = Encoding.UTF8.GetBytes(sendJson);
					await webSocket.SendAsync(new ArraySegment<byte>(sendBytes), receive.MessageType, true, CancellationToken.None);
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
