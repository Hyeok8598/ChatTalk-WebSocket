using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using ChatTalk.WebServer.Data.Dapper.Sql;
using Dapper;

namespace ChatTalk.WebServer.Data.Dapper.Repositoy
{
    public class UsersRepository
    {
        private readonly DbConnectionFactory _factory;

        public UsersRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<UsersEntity?> SelectOne001(UsersDto usersDto)
        {
            var conn = _factory.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<UsersEntity>(UsersSql.SELECT_ONE_001, usersDto);
        }
}}
