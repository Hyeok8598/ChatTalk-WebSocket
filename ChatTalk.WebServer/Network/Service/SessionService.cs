using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
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

            UsersDto usersDto = new UsersDto { UserId = joinMessage.UserId };
            UsersEntity? user = await _usersService.SelectOne001(usersDto);

            if (user == null)
            {
                _logger.LogWarning(
                    "[USER NOT FOUND] UserId={UserId}",
                    usersDto.UserId
                );

                return;
            }

            handler.UserInfo.SetUserName(user.UserName);
            handler.UserInfo.SetUserId(user.UserId);
            _clientManager.Add(handler.GetConnectId(), handler);

            await _sendService.BroadcastAsync(new UserListMessage
            {
                Users = _clientManager.GetAllUserInfo()
            });

            return;
        }
    }
}
