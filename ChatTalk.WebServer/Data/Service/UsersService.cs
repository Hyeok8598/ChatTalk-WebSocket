using ChatTalk.WebServer.Data.Entities;
using ChatTalk.WebServer.Data.Repository;

namespace ChatTalk.WebServer.Data.Service
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository;

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UsersEntity?> FindJoinUserAsync(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId)) return null;

            return await _usersRepository.FindByUserIdAsync(userId);
        }
}
}
