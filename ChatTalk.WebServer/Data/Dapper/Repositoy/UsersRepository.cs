using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using ChatTalk.WebServer.Data.Dapper.Sql;
using ChatTalk.Common;
using Dapper;
using ChatTalk.Common.Log;

namespace ChatTalk.WebServer.Data.Dapper.Repositoy
{
    public class UsersRepository
    {
        private readonly DbConnectionFactory _factory;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(DbConnectionFactory factory, ILogger<UsersRepository> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task<UsersEntity?> SelectOne001(UsersDto dto)
        {
            var sql = UsersSql.SELECT_ONE_001;
            _logger.LogInformation(SqlLogger.Format(sql, dto));

            var conn = _factory.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<UsersEntity>(sql, dto);
        }
}}