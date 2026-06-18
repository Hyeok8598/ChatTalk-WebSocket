using System.Net.WebSockets;

namespace ChatTalk.WebServer.Network.Service
{
    public class WebSocketAcceptService
    {
        private readonly ILogger<WebSocketAcceptService> _logger;
        private readonly WebSocketReceiveService _webSocketReceiveService;

        public WebSocketAcceptService(ILogger<WebSocketAcceptService> logger, WebSocketReceiveService webSocketReceiveService)
        {
            _logger = logger;
            _webSocketReceiveService = webSocketReceiveService;
        }

        public async Task AccecptAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                _logger.LogWarning(
                    "[INVALID REQUEST] Path={Path}, RemoteIp={RemoteIp}",
                    context.Request.Path,
                    context.Connection.RemoteIpAddress
                );

                return;
            }

            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            WebSocketHandler handler = new WebSocketHandler(webSocket);
            
            _logger.LogInformation(
                "[CONNECTED] ConnectedId={ConnectedId}",
                handler.GetConnectId()
            );

            await _webSocketReceiveService.ReceiveAsync(handler);
        }
    }
}
