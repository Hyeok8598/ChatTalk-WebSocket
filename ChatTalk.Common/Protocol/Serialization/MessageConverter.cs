using ChatTalk.Common.Protocol.Constant;
using ChatTalk.Common.Protocol.Messages;
using System.Text.Json;

namespace ChatTalk.Common.Protocol.Serialization
{
    public static class MessageConverter
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static BaseMessage? Create(string json)
        {

        var baseMsg = JsonSerializer.Deserialize<BaseMessage>(json, Options);
            if(baseMsg == null) return null;

            return baseMsg.Type switch
            {
                MessageType.Msg => JsonSerializer.Deserialize<ChatMessage>(json),
                MessageType.Whisper => JsonSerializer.Deserialize<WhisperMessage>(json),
                MessageType.UserList => JsonSerializer.Deserialize<UserListMessage>(json),
                MessageType.Join => JsonSerializer.Deserialize<JoinMessage>(json),
                MessageType.Leave => JsonSerializer.Deserialize<LeaveMessage>(json),
                MessageType.System => JsonSerializer.Deserialize<SystemMessage>(json),

                _ => null
            };
        }
    }
}
