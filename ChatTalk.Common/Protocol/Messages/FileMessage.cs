using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTalk.Common.Protocol.Messages
{
    public class FileMessage : BaseMessage
    {
        public string MessageId { get; set; } = string.Empty;
        public string SenderUserId { get; set; } = string.Empty;
        public string RefType { get; set; } = string.Empty;
        public string RefId { get; set; } = string.Empty;
        public string OriginalName { get; set; } = string.Empty;
        public string StoredName { get; set; } = string.Empty;
        public string filePath { get; set; } = string.Empty;

        public FileMessage()
        {
            Type = "FILE";
        }
    }
}
