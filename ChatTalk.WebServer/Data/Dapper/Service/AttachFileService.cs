using ChatTalk.WebServer.Data.Dapper.Dto;
using ChatTalk.WebServer.Data.Dapper.Repository;
using ChatTalk.WebServer.Data.Dapper.Repositoy;

namespace ChatTalk.WebServer.Data.Dapper.Service
{
    public class AttachFileService
    {
        private readonly AttachFileRepository _attachFileRepository;

        public AttachFileService(AttachFileRepository attachFileRepository)
        {
            _attachFileRepository = attachFileRepository;
        }

        public async Task<AttachFileDto?> SelectOne001(AttachFileDto dto)
        {
            return await _attachFileRepository.SelectOne001(dto);
        }
    }
}
