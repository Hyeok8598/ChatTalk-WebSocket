namespace ChatTalk.WebServer.Data.Dapper.Dto
{
    public class AttachFileDto
    {
        public long Id { get; set; }
        public string RefType { get; set; } = string.Empty;
        public string RefId { get; set; } = string.Empty;
        public string OriginalName { get; set; } = string.Empty;
        public string StoredName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public string CotentType { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public string CreateBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
