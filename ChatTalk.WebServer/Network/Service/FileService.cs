using ChatTalk.Common.Protocol.Messages;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Service;
using ChatTalk.WebServer.Data.EfCore.Service;

namespace ChatTalk.WebServer.Network.Service
{
    public class FileService
    {
        private ILogger<FileService> _logger;

        private readonly WebSocketSendService _webSocketSendService;
        private readonly UsersService _usersService;
        private readonly AttachFileService _attachFileService;
        private readonly ChatMessageService _chatMessageService;

        public FileService(ILogger<FileService> logger, WebSocketSendService webSocketSendService, UsersService usersService, AttachFileService attachFileService, ChatMessageService chatMessageService)
        {
            _logger = logger;
            _webSocketSendService = webSocketSendService;
            _usersService = usersService;
            _attachFileService = attachFileService;
            _chatMessageService = chatMessageService;
        }

        public async Task SendAsync(WebSocketHandler handler, BaseMessage baseMessage)
        {
            FileMessage fileMessage = (FileMessage)baseMessage;
            UsersDto usersInputDto = new UsersDto
            {
                UserId = fileMessage.SenderUserId
            };

            UsersDto? usersOutDto = await _usersService.SelectOne001(usersInputDto);

            if (usersOutDto == null)
            {
                _logger.LogWarning("[USER NOT FOUND] UserId={UserId}", usersInputDto.UserId);
                return;
            }

            AttachFileDto fileInputDto = new AttachFileDto
            {
                RefId = fileMessage.RefId,
                RefType = fileMessage.RefType
            };
            AttachFileDto? fileOutDto = await _attachFileService.SelectOne001(fileInputDto);

            if (fileOutDto == null)
            {
                _logger.LogWarning("[USER NOT FOUND] UserId={UserId}", usersInputDto.UserId);
                return;
            }

            ChatMessageDto msgInDto = new ChatMessageDto
            {
                MessageId = fileMessage.MessageId,
                MessageType = fileMessage.Type,
                SenderUserId = fileMessage.SenderUserId
            };
            await _chatMessageService.Insert001(msgInDto);

            fileMessage.OriginalName = fileOutDto.OriginalName;
            fileMessage.StoredName = fileOutDto.StoredName;
            fileMessage.filePath = fileOutDto.FilePath;

            await _webSocketSendService.BroadcastAsync(fileMessage);
        }
    }
}
