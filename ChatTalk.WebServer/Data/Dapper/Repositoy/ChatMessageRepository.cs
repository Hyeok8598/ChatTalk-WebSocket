using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Entities;
using ChatTalk.WebServer.Data.Dapper.Sql;
using ChatTalk.WebServer.Data.EfCore;
using Dapper;

namespace ChatTalk.WebServer.Data.Dapper.Repository
{
    public class ChatMessageRepository
    {
        private readonly DbConnectionFactory _factory;

        public ChatMessageRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task Insert001(ChatMessageEntity entity)
        {
            var conn = _factory.CreateConnection();
            await conn.QuerySingleOrDefaultAsync<ChatMessageEntity>(ChatMessageSql.INSERT_001, entity);
        }
    }
}