using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using ChatTalk.WebServer.Data.Dapper.Service;

namespace ChatTalk.WebServer.Network.Service
{
    public class WhisperService
    {
        private readonly ILogger<WhisperService> _logger;

        private readonly WebSocketSendService _websocketSendService;
        private readonly UsersService _userService;

        public WhisperService(ILogger<WhisperService> logger, WebSocketSendService websocketSendService, UsersService userService)
        {
            _logger = logger;
            _websocketSendService = websocketSendService;
            _userService = userService;
        }

        public async Task SendAsyc(WebSocketHandler handler, BaseMessage baseMessage)
        {
            WhisperMessage whisperMessage = (WhisperMessage)baseMessage;
            UsersDto usersDto = new UsersDto { UserId = whisperMessage.Target };
            UsersEntity? user = await _userService.SelectOne001(usersDto);

            if (user == null)
            {
                _logger.LogWarning(
                    "[USER NOT FOUND] UserId={UserId}",
                    usersDto.UserId
                );

                return;
            }

            await _websocketSendService.SendToClientAsync(baseMessage, whisperMessage.Target);
        }
    }
}