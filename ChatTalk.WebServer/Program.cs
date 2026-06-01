using ChatTalk.WebServer;
using ChatTalk.WebServer.Network;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

var webSocketServer = new WebSocketServer();

app.Map("/ws", async (HttpContext context) =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = 400;
        return;
    }

    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
    await webSocketServer.AcceptAsync(webSocket);
});

app.Run();