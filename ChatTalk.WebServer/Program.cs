using ChatTalk.WebServer.Data.Dapper;
using ChatTalk.WebServer.Data.Dapper.Repository;
using ChatTalk.WebServer.Data.Dapper.Repositoy;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;
using ChatTalk.WebServer.Network;
using ChatTalk.WebServer.Network.Service;

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
builder.Services.AddScoped<AttachFileRepository>();
builder.Services.AddScoped<AttachFileService>();

builder.Services.AddSingleton<ClientManager>();
builder.Services.AddScoped<WebSocketServer>();

builder.Services.AddScoped<MessageDispatcherService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<WebSocketAcceptService>();
builder.Services.AddScoped<WebSocketReceiveService>();
builder.Services.AddScoped<WebSocketSendService>();
builder.Services.AddScoped<WhisperService>();
builder.Services.AddScoped<SystemService>();
builder.Services.AddScoped<FileService>();

var app = builder.Build();

app.UseWebSockets();

app.Map("/ws", async (HttpContext context, WebSocketServer webSocketServer) =>
{
    await webSocketServer.AcceptAsync(context);
});

app.Run();