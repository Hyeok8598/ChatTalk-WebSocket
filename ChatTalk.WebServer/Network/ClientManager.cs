using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace ChatTalk.WebServer.Network
{
    public class ClientManager
    {
        private readonly ConcurrentDictionary<string, WebSocketHandler> _clients = new();

        public void Add(string userName, WebSocketHandler handler)
        {
            _clients.TryAdd(userName, handler);
        }

        public bool Remove(string userName)
        {
            return _clients.TryRemove(userName, out _);
        }

        public IReadOnlyCollection<string> GetAllClients()
        {
            return _clients.Keys.ToList();
        }

        public Array getAllHandlers()
        {
            return _clients.Values.ToArray();
        }
    }
}
