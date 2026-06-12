using ChatTalk.WebServer.Data.Dapper;
using ChatTalk.WebServer.Data.Dapper.Repository;
using ChatTalk.WebServer.Data.Dapper.Repositoy;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;
using ChatTalk.WebServer.Network;

var builder = WebApplication.CreateBuilder(args);

/* 26.06.10 - DB 연동 */
/* 26.06.13 - Dapper 사용으로 인해 EF Core 임시 주석 */
//builder.Services.AddDbContext<ChatTalkDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatTalkDb"));
//    options.EnableSensitiveDataLogging();
//});
builder.Services.AddSingleton<DbConnectionFactory>();
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