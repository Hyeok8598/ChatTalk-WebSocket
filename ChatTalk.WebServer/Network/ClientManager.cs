using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace ChatTalk.WebServer.Network
{
    public class ClientManager
    {
        private readonly ConcurrentDictionary<string, WebSocketHandler> _clients = new();

        public void Add(string userId, WebSocketHandler handler)
        {
            _clients.TryAdd(userId, handler);
        }

        public bool Remove(string userId)
        {
            return _clients.TryRemove(userId, out _);
        }

        public Array GetAllHandlers()
        {
            return _clients.Values.ToArray();
        }
    }
}