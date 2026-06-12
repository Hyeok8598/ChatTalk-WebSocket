using ChatTalk.WebServer;
using ChatTalk.WebServer.Data;
using ChatTalk.WebServer.Data.Repository;
using ChatTalk.WebServer.Data.Service;
using ChatTalk.WebServer.Network;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/* 26.06.10 - DB 연동 */
builder.Services.AddDbContext<ChatTalkDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatTalkDb"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<ChatMessageRepository>();
builder.Services.AddScoped<ChatMessageService>();
builder.Services.AddScoped<WebSocketServer>();

var app = builder.Build();

app.UseWebSockets();

app.Map("/ws", async (HttpContext context, WebSocketServer webSocketServer) =>
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