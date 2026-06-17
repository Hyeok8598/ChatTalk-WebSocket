using ChatTalk.Common.Protocol.Model;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace ChatTalk.WebServer.Network
{
    public class ClientManager
    {
        private readonly ConcurrentDictionary<string, WebSocketHandler> _clients = new();

        public void Add(string connectedId, WebSocketHandler handler)
        {
            _clients.TryAdd(connectedId, handler);
        }

        public bool Remove(string connectedId)
        {
            return _clients.TryRemove(connectedId, out _);
        }

        public UserInfo[] GetAllUserInfo()
        {
            foreach (var handler in _clients.Values)
            {
                Console.WriteLine(
                    $"[Client] UserId={handler.UserInfo.UserId}, UserName={handler.UserInfo.UserName}"
                );
            }

            return _clients.Values
                .Select(handler => handler.UserInfo)
                .Where(user => !string.IsNullOrEmpty(user.UserId))
                .ToArray();
        }

        public WebSocketHandler? GetHandler(string userId)
        {
            WebSocketHandler? h = _clients.Values.FirstOrDefault(
                    handler => handler.UserInfo.UserId == userId
                );
            Console.WriteLine($"userID: {h?.UserInfo.UserId}");
            return h;
        }

        public Array GetAllHandlers()
        {
            return _clients.Values.ToArray();
        }
    }
}