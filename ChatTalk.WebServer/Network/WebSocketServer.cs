using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Serialization;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;
using System.Net.WebSockets;
using System.Text;

namespace ChatTalk.WebServer.Network;

public class WebSocketServer
{
	private static readonly ClientManager clients = new();
	private readonly UsersService _usersService;
	private readonly ChatMessageService _chatMessageService;

	public WebSocketServer(UsersService usersService, ChatMessageService chatMessageService)
	{
		_usersService = usersService;
        _chatMessageService = chatMessageService;
    }

	public async Task AcceptAsync(WebSocket webSocket)
	{
        Console.WriteLine("[Connected] : WebSocket opened");
		var handler = new WebSocketHandler(this, webSocket, _usersService, _chatMessageService);

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
		Array handlers = clients.GetAllHandlers();
		foreach (WebSocketHandler handler in handlers)
		{
			await handler.SendAsync(sendBytes);
        }
    }

	public void AddClient(string userId, WebSocketHandler handler)
	{
        clients.Add(userId, handler);
    }

	public void RemoveClient(string userId)
	{
		clients.Remove(userId);
	}

	public string[] GetUserNames()
	{
		Array handlers = clients.GetAllHandlers();
        string[] userNames = new string[handlers.Length];

		int idx = 0;
		foreach(WebSocketHandler handler in handlers)
		{
			string userName = handler.GetUserName();
			userNames[idx++] = userName;
        }

		return userNames;
    }
}