using ChatTalk.Common.Protocol.Messages;
using ChatTalk.Common.Protocol.Model;
using ChatTalk.Common.Protocol.Serialization;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatTalk.WebServer.Network.Service
{
    public class MessageDispatcherService
    {
        private readonly ILogger<MessageDispatcherService> _logger;
        private readonly SessionService _sessionService;
        private readonly MessageService _messageService;
        private readonly WhisperService _whisperService;

        public MessageDispatcherService(
            ILogger<MessageDispatcherService> logger,
            SessionService sessionService,
            MessageService messageService,
            WhisperService whisperService
        )
            
        {
            _logger = logger;
            _sessionService = sessionService;
            _messageService = messageService;
            _whisperService = whisperService;
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
            }

            if (baseMessage is LeaveMessage leaveMessage)
            {
                //await _sessionService.JoinAsync(handler, baseMessage);
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
