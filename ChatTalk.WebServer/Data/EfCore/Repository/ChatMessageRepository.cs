//using ChatTalk.WebServer.Data.Dapper.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace ChatTalk.WebServer.Data.EfCore.Repository
//{
//    public class ChatMessageRepository
//    {
//        private readonly ChatTalkDbContext _dbContext;

//        public ChatMessageRepository(ChatTalkDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task InsertAsync(ChatMessageEntity chatEntity)
//        {
//            _dbContext.ChatMessages.Add(chatEntity);
//            await _dbContext.SaveChangesAsync();
//        }
//    }
//}