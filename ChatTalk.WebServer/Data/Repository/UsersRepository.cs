using ChatTalk.WebServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatTalk.WebServer.Data.Repository
{
    public class UsersRepository
    {
        private readonly ChatTalkDbContext _dbContext;

        public UsersRepository(ChatTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UsersEntity> FindByUserIdAsync(string userId) 
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
