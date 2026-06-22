using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Serialization;
using System.Net.WebSockets;
using System.Text;

namespace ChatTalk.WebServer.Network.Service
{
    public class WebSocketReceiveService
    {
        private readonly ILogger<WebSocketReceiveService> _logger;
        private readonly MessageDispatcherService _messageDispatcherService;
        private readonly SessionService _sessionService;

        public WebSocketReceiveService (ILogger<WebSocketReceiveService> logger, MessageDispatcherService messageDispatcherService, SessionService sessionService)
        {
            _logger = logger;
            _messageDispatcherService = messageDispatcherService;
            _sessionService = sessionService;
        }

        public async Task ReceiveAsync(WebSocketHandler handler)
        {
            var buffer = new byte[1024];

            try
            {
                while (handler.GetWebSocket().State == WebSocketState.Open)
                {
                    WebSocketReceiveResult receive = await handler.GetWebSocket().ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (receive.MessageType == WebSocketMessageType.Close)
                    {
                        _logger.LogInformation(
                            "[DISCONNECT] ConnectionId={ConnectionId}",
                            handler.GetConnectId()
                        );

                        /* 소켓 종료 로직 구현 */
                        return;
                    }
                    else
                    {
                        string json = Encoding.UTF8.GetString(buffer, 0, receive.Count);

                        _logger.LogInformation(
                            "[RECEIVE] ConnectionId={ConnectionId} Json={Json}",
                            handler.GetConnectId(),
                            json
                        );

                        BaseMessage? message = MessageConverter.Create(json);

                        if (message == null)
                        {
                            _logger.LogWarning(
                                "[INVALID_MESSAGE] Json={Json}",
                                json
                            );

                            return;
                        }

                        await _messageDispatcherService.DispatcherAsync(handler, message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "[SOCKET DISCONNECT]");
                _logger.LogInformation(
                    "[RECEIVE] ConnectionId={ConnectionId}",
                    handler.GetConnectId()
                );
            }
            finally
            {
                await _sessionService.DisconnectAsync(handler);
            }
    }
}}
