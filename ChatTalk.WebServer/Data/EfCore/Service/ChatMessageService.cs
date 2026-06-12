//using ChatTalk.WebServer.Data.EfCore.Entities;
//using ChatTalk.WebServer.Data.EfCore.Repository;

//namespace ChatTalk.WebServer.Data.EfCore.Service
//{
//    public class ChatMessageService
//    {
//        private readonly ChatMessageRepository _chatMessageRepository;

//        public ChatMessageService(ChatMessageRepository chatMessageRepository)
//        {
//            _chatMessageRepository = chatMessageRepository;
//        }

//        public async Task SaveMessageAsync(ChatMessageEntity entity)
//        {
//            await _chatMessageRepository.InsertAsync(entity);
//        }
//    }
//}