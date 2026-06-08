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
                MessageType.Msg => JsonSerializer.Deserialize<ChatMessage>(json, Options),
                MessageType.Whisper => JsonSerializer.Deserialize<WhisperMessage>(json, Options),
                MessageType.UserList => JsonSerializer.Deserialize<UserListMessage>(json, Options),
                MessageType.Join => JsonSerializer.Deserialize<JoinMessage>(json, Options),
                MessageType.Leave => JsonSerializer.Deserialize<LeaveMessage>(json, Options),
                MessageType.System => JsonSerializer.Deserialize<SystemMessage>(json, Options),

                _ => null
            };
        }
    }
}
