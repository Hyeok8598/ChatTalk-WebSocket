using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;

namespace ChatTalk.WebServer.Network.Service
{
    public class MessageService
    {
        private ILogger<MessageService> _logger;

        private readonly WebSocketSendService _webSocketSendService;
        private readonly ChatMessageService _chatMessageService;
        private readonly UsersService _usersService;

        public MessageService(ILogger<MessageService> logger, WebSocketSendService webSocketSendService, ChatMessageService chatMessageService, UsersService usersService)
        {
            _logger = logger;
            _webSocketSendService = webSocketSendService;
            _chatMessageService = chatMessageService;
            _usersService = usersService;
        }

        public async Task SendAsync(WebSocketHandler handler, BaseMessage baseMessage)
        {
            ChatMessage chatMessage = (ChatMessage)baseMessage;
            ChatMessageDto chatMessageDto = new ChatMessageDto
            {
                MessageId = chatMessage.MessageId,
                SenderUserId = chatMessage.SenderUserId,
                MessageType = chatMessage.Type,
                Content = chatMessage.Content
            };

            UsersDto usersInputDto = new UsersDto
            {
                UserId = chatMessage.SenderUserId
            };

            UsersDto? usersOutDto = await _usersService.SelectOne001(usersInputDto);

            if (usersOutDto == null)
            {
                _logger.LogWarning("[USER NOT FOUND] UserId={UserId}", usersInputDto.UserId);
                return;
            }

            await _chatMessageService.Insert001(chatMessageDto);
            await _webSocketSendService.BroadcastAsync(chatMessage);
        }
    }
}
