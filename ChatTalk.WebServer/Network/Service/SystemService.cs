using ChatTalk.Common.Protocol.Constant;
using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Service;

namespace ChatTalk.WebServer.Network.Service
{
    public class SystemService
    {
        private readonly ILogger<SystemService> _logger;

        private readonly WebSocketSendService _webSocketSendService;
        private readonly UsersService _userService;

        public SystemService(ILogger<SystemService> logger, WebSocketSendService webSocketSendService, UsersService userService)
        {
            _logger = logger;
            _webSocketSendService = webSocketSendService;
            _userService = userService;
        }

        private async Task SendAsyc(WebSocketHandler handler, SystemMessage systemMessage)
        {
            UsersDto? user = await _userService.SelectOne001(
                new UsersDto
                {
                    UserId = systemMessage.UserId
                });

            if (user == null)
            {
                _logger.LogWarning(
                    "[USER NOT FOUND] UserId={UserId}",
                    systemMessage.UserId
                );

                return;
            }

            systemMessage.UserName = user.UserName;
            await _webSocketSendService.BroadcastAsync(systemMessage);
        }

        public async Task SendJoinAsync(WebSocketHandler handler, JoinMessage joinMessage)
        {
            SystemMessage systemMessage = new SystemMessage
            {
                Type = MessageType.System,
                SystemType = joinMessage.Type,
                UserId = joinMessage.SenderUserId,
            };
            await SendAsyc(handler, systemMessage);
        }

        public async Task SendLeaveAsync(WebSocketHandler handler, LeaveMessage leaveMessage)
        {
            SystemMessage systemMessage = new SystemMessage
            {
                Type = MessageType.System,
                SystemType = leaveMessage.Type,
                UserId = leaveMessage.SenderUserId,
            };
            await SendAsyc(handler, systemMessage);
        }
    }
}
