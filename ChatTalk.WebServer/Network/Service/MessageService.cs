using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.EfCore.Service;
using Microsoft.AspNetCore.Hosting.Server;

namespace ChatTalk.WebServer.Network.Service
{
    public class MessageService
    {
        private ILogger<MessageService> _logger;

        private readonly WebSocketSendService _webSocketSendService;
        private readonly ChatMessageService _chatMessageService;

        public MessageService(ILogger<MessageService> logger, WebSocketSendService webSocketSendService, ChatMessageService chatMessageService)
        {
            _logger = logger;
            _webSocketSendService = webSocketSendService;
            _chatMessageService = chatMessageService;
        }

        public async Task SendAsync(WebSocketHandler handler, BaseMessage baseMessage)
        {
            ChatMessage chatMessage = (ChatMessage)baseMessage;
            ChatMessageDto chatMessageDto = new ChatMessageDto
            {
                SenderName = chatMessage.Sender,
                MessageId = chatMessage.MessageId,
                Content = chatMessage.Content
            };

            await _chatMessageService.Insert001(chatMessageDto);
            await _webSocketSendService.BroadcastAsync(baseMessage);
        }
    }
}
