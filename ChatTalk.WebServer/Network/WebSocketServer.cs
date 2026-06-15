using ChatTalk.Common.Protocol.Model;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;
using System.Net.WebSockets;

namespace ChatTalk.WebServer.Network;

public class WebSocketServer
{
	public ClientManager Clients { get; }
	public UsersService UsersService { get; }
	public ChatMessageService ChatMessageService { get; }

	public WebSocketServer(UsersService usersService, ChatMessageService chatMessageService)
	{
        Console.WriteLine($"[ClientManager] Hash={GetHashCode()}");

        Clients = new ClientManager();
        UsersService = usersService;
        ChatMessageService = chatMessageService;
    }

	public async Task AcceptAsync(WebSocket webSocket)
	{
        Console.WriteLine("[Connected] : WebSocket opened");
		var handler = new WebSocketHandler(this, webSocket);

		try
		{
			while(webSocket.State == WebSocketState.Open)
			{
				await handler.RunAsync();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[Error] : {ex.Message}");
		}

        Console.WriteLine("[Disconnected] WebSocket closed");
    }

	public async Task BroadCastAsync(byte[] sendBytes)
	{
		Array handlers = Clients.GetAllHandlers();
		foreach (WebSocketHandler handler in handlers)
		{
			await handler.SendAsync(sendBytes);
        }
    }
}