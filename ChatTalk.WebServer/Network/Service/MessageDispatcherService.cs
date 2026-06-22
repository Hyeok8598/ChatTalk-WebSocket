using ChatTalk.Common.Protocol.Messages;

namespace ChatTalk.WebServer.Network.Service
{
    public class MessageDispatcherService
    {
        private readonly ILogger<MessageDispatcherService> _logger;
        private readonly SessionService _sessionService;
        private readonly MessageService _messageService;
        private readonly WhisperService _whisperService;
        private readonly SystemService _systemService;

        public MessageDispatcherService(
            ILogger<MessageDispatcherService> logger,
            SessionService sessionService,
            MessageService messageService,
            WhisperService whisperService,
            SystemService systemService
        )
            
        {
            _logger = logger;
            _sessionService  = sessionService;
            _messageService  = messageService;
            _whisperService  = whisperService;
            _systemService   = systemService;
        }

        public async Task DispatcherAsync(WebSocketHandler handler, BaseMessage baseMessage)
        {
            if (baseMessage == null)
            {
                return;
            }

            if (baseMessage is JoinMessage joinMessage)
            {
                await _sessionService.JoinAsync(handler, baseMessage);
                await _systemService.SendJoinAsync(handler, joinMessage);
            }

            if (baseMessage is LeaveMessage leaveMessage)
            {
                await _systemService.SendLeaveAsync(handler, leaveMessage);
                await _sessionService.LeaveAsync(handler, baseMessage);
            }

            if (baseMessage is WhisperMessage whisperMessage)
            {
                await _whisperService.SendAsyc(handler, baseMessage);
            }

            if (baseMessage is ChatMessage chatMessage)
            {
                await _messageService.SendAsync(handler, baseMessage);
            }
        }
    }
}
