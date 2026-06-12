using ChatTalk.WebServer.Data.Entities;
using ChatTalk.WebServer.Data.Repository;

namespace ChatTalk.WebServer.Data.Service
{
    public class ChatMessageService
    {
        private readonly ChatMessageRepository _chatMessageRepository;

        public ChatMessageService(ChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task SaveMessageAsync(ChatMessageEntity entity)
        {
            await _chatMessageRepository.InsertAsync(entity);
        }
    }
}
