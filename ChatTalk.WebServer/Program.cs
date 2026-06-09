using ChatTalk.WebServer;
using ChatTalk.WebServer.Data;
using ChatTalk.WebServer.Network;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/* 26.06.10 - DB 연동 */
builder.Services.AddDbContext<ChatTalkDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatTalkDb"));
});

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