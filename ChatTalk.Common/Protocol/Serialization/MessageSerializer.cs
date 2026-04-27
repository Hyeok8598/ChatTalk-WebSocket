using ChatTalk.Common.Protocol.Constant;
using ChatTalk.Common.Protocol.Messages;
using System.Text.Json;

namespace ChatTalk.Common.Protocol.Serialization
{
    public static class MessageSerializer
    {
        public static readonly JsonSerializerOptions Option = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string Serialize(BaseMessage baseMessage)
        {
            return JsonSerializer.Serialize(baseMessage, baseMessage.GetType(), Option);
        }
    }
}
