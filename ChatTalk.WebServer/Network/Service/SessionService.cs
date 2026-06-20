using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Service;

namespace ChatTalk.WebServer.Network.Service
{
    public class SessionService
    {
        private readonly ILogger _logger;
        private readonly ClientManager _clientManager;
        private readonly WebSocketSendService _sendService;
        private readonly UsersService _usersService;

        public SessionService(ILogger<SessionService> logger, UsersService usersService, WebSocketSendService sendService, ClientManager clientManager)
        {
            _logger = logger;
            _usersService = usersService;
            _sendService = sendService;
            _clientManager = clientManager;
        }

        public async Task JoinAsync(WebSocketHandler handler, BaseMessage baseMassage)
        {
            JoinMessage joinMessage = (JoinMessage)baseMassage;

            UsersDto userInputDto = new UsersDto { UserId = joinMessage.UserId };
            UsersDto? userOutDto = await _usersService.SelectOne001(userInputDto);

            if (userOutDto == null)
            {
                _logger.LogWarning(
                    "[USER NOT FOUND] UserId={UserId}",
                    userInputDto.UserId
                );

                return;
            }

            handler.UserInfo.SetUserName(userOutDto.UserName);
            handler.UserInfo.SetUserId(userOutDto.UserId);
            _clientManager.Add(handler.GetConnectId(), handler);

            await _sendService.BroadcastAsync(new UserListMessage
            {
                Users = _clientManager.GetAllUserInfo()
            });

            return;
        }
    }
}
