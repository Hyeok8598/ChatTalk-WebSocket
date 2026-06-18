using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;
using ChatTalk.WebServer.Network.Service;

namespace ChatTalk.WebServer.Network;

public class WebSocketServer
{
	public ClientManager Clients { get; }
	public UsersService UsersService { get; }
	public ChatMessageService ChatMessageService { get; }

	private readonly WebSocketAcceptService _webSocketAcceptService;

	public WebSocketServer(UsersService usersService, ChatMessageService chatMessageService, WebSocketAcceptService webSocketAcceptService)
	{
        Console.WriteLine($"[ClientManager] Hash={GetHashCode()}");

        Clients = new ClientManager();
        UsersService = usersService;
        ChatMessageService = chatMessageService;
        _webSocketAcceptService = webSocketAcceptService;
    }

	public async Task AcceptAsync(HttpContext httpContext)
	{
		await _webSocketAcceptService.AccecptAsync(httpContext);
  //      Console.WriteLine("[Connected] : WebSocket opened");
  //var handler = new WebSocketHandler(this, webSocket);

        //try
        //{
        //	while(webSocket.State == WebSocketState.Open)
        //	{
        //		await handler.RunAsync();
        //	}
        //}
        //catch (Exception ex)
        //{
        //	Console.WriteLine($"[Error] : {ex.Message}");
        //}

        //      Console.WriteLine("[Disconnected] WebSocket closed");
    }
}