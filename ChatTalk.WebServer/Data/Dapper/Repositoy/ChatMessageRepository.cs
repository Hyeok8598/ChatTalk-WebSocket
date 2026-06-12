using ChatTalk.Common.Log;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using ChatTalk.WebServer.Data.Dapper.Repositoy;
using ChatTalk.WebServer.Data.Dapper.Sql;
using Dapper;

namespace ChatTalk.WebServer.Data.Dapper.Repository
{
    public class ChatMessageRepository
    {
        private readonly ILogger<ChatMessageRepository> _logger;
        private readonly DbConnectionFactory _factory;

        public ChatMessageRepository(DbConnectionFactory factory, ILogger<ChatMessageRepository> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task Insert001(ChatMessageDto dto)
        {
            var sql = ChatMessageSql.INSERT_001;
            _logger.LogInformation(SqlLogger.Format(sql, dto));

            var conn = _factory.CreateConnection();
            await conn.QuerySingleOrDefaultAsync<ChatMessageEntity>(ChatMessageSql.INSERT_001, dto);
        }
    }
}