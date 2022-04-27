using ConsoleChat.Core.DI;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleChat.Server.Client;

public class ClientStore
{
    private readonly object _lock = new object();
    private readonly Dictionary<Guid, ClientSocket> _clients = new Dictionary<Guid, ClientSocket>();

    private static readonly Lazy<ClientStore> _instance = new Lazy<ClientStore>(() => new ClientStore());
    public static ClientStore Instance => _instance.Value;

    // for internal creation only
    private ClientStore()
    {

    }

    public int GetClientCount()
    {
        lock (_lock)
        {
            return _clients.Count;
        }
    }

    public void RemoveClient(Guid clientId)
    {
        lock (_lock)
        {
            var client = _clients[clientId];
            client.ClientDisconnected -= RemoveClient;
            _clients.Remove(clientId);
        }
    }

    public void AddClient(ClientSocket client)
    {
        lock (_lock)
        {
            client.ClientDisconnected += RemoveClient;
            _clients.Add(client.Id, client);
        }
    }

    public ClientSocket[] GetClients()
    {
        lock (_lock)
        {
            return _clients.Values.ToArray();
        }
    }

}

