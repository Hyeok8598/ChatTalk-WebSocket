using ChatTalk.Common.Log;
using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Repository;
using ChatTalk.WebServer.Data.Dapper.Sql;
using Dapper;

namespace ChatTalk.WebServer.Data.Dapper.Repositoy
{
    public class AttachFileRepository
    {
        private readonly ILogger<AttachFileRepository> _logger;
        private readonly DbConnectionFactory _factory;

        public AttachFileRepository(DbConnectionFactory factory, ILogger<AttachFileRepository> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task<AttachFileDto?> SelectOne001(AttachFileDto dto)
        {
            var sql = AttachFileSql.SELECT_ONE_001;
            _logger.LogInformation(SqlLogger.Format(sql, dto));

            var conn = _factory.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<AttachFileDto>(sql, dto);
        }
    }
}
