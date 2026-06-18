using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Serialization;
using System.Text;

namespace ChatTalk.WebServer.Network.Service
{
    public class WebSocketSendService
    {
        private readonly ILogger<WebSocketSendService> _logger;
        private readonly ClientManager _clientManager;

        public WebSocketSendService(ILogger<WebSocketSendService> logger, ClientManager clientManager)
        {
            _logger = logger;
            _clientManager = clientManager;
        }

        public async Task BroadcastAsync(BaseMessage baseMessage)
        {
            foreach (WebSocketHandler handler in _clientManager.GetAllHandlers())
            {
                await handler.SendAsync(baseMessage);
            }
        }

        public async Task SendToClientAsync(BaseMessage baseMessage, String userId)
        {
            WebSocketHandler? handler = _clientManager.GetHandler(userId);

            if (handler == null)
            {
                _logger.LogWarning(
                    "[USER NOT CONNECTED] UserId={UserId}",
                    userId
                );

                return;
            }

            await handler.SendAsync(baseMessage);
        }
    }
}
