using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using ChatTalk.WebServer.Data.Dapper.Repositoy;

namespace ChatTalk.WebServer.Data.Dapper.Service
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository;

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UsersEntity?> SelectOne001(UsersDto dto)
        {
            //if(string.IsNullOrWhiteSpace(userId)) return null;

            return await _usersRepository.SelectOne001(dto);
        }
}
}
