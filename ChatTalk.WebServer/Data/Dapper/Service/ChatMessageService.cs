
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Repository;

namespace ChatTalk.WebServer.Data.EfCore.Service
{
    public class ChatMessageService
    {
        private readonly ChatMessageRepository _chatMessageRepository;

        public ChatMessageService(ChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task Insert001(ChatMessageDto dto)
        {
            await _chatMessageRepository.Insert001(dto);
        }
    }
}
