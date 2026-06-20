using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;

namespace ChatTalk.WebServer.Network.Service
{
    public class WhisperService
    {
        private readonly ILogger<WhisperService> _logger;

        private readonly WebSocketSendService _websocketSendService;
        private readonly UsersService _userService;
        private readonly ChatMessageService _chatMessageService;

        public WhisperService(ILogger<WhisperService> logger, WebSocketSendService websocketSendService, UsersService userService, ChatMessageService chatMessageService)
        {
            _logger = logger;
            _websocketSendService = websocketSendService;
            _userService = userService;
            _chatMessageService = chatMessageService;
        }

        public async Task SendAsyc(WebSocketHandler handler, BaseMessage baseMessage)
        {
            WhisperMessage whisperMessage = (WhisperMessage)baseMessage;
            ChatMessageDto chatMessageInsert001InDto;

            var senderTask = _userService.SelectOne001(
                new UsersDto
                {
                    UserId = whisperMessage.SenderUserId
                });

            var targetTask = _userService.SelectOne001(
                new UsersDto
                {
                    UserId = whisperMessage.TargetUserId
                });

            await Task.WhenAll(senderTask, targetTask);

            var senderUser = await senderTask;
            var targetUser = await targetTask;

            if (senderUser == null)
            {
                _logger.LogWarning(
                    "[USER NOT FOUND] UserId={UserId}",
                    whisperMessage.SenderUserId
                );

                return;
            }

            if (targetUser == null)
            {
                _logger.LogWarning(
                    "[USER NOT FOUND] UserId={UserId}",
                    whisperMessage.TargetUserId
                );

                return;
            }

            chatMessageInsert001InDto = new ChatMessageDto
            {
                MessageId = whisperMessage.MessageId,
                MessageType = whisperMessage.Type,
                SenderUserId = whisperMessage.SenderUserId,
                TargetUserId = whisperMessage.TargetUserId,
                Content = whisperMessage.Content
            };

            whisperMessage.SenderUserName = senderUser.UserName;
            whisperMessage.TargetUserName = targetUser.UserName;

            await _chatMessageService.Insert001(chatMessageInsert001InDto);
            await _websocketSendService.SendToClientAsync(whisperMessage, whisperMessage.TargetUserId);
        }
    }
}