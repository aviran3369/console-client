using System.Net.Sockets;

namespace ConsoleChat.Server.Client;

public delegate void ClientDisconnectedEvent(Guid clientId);

public class ClientSocket
{
    public event ClientDisconnectedEvent ClientDisconnected;

    public Guid Id { get;}
    public TcpClient Socket { get; }

    public ClientSocket(Guid id, TcpClient socket)
    {
        Id = id;
        Socket = socket;
    }

    public void Disconnect()
    {
        ClientDisconnected(Id);
    }
}




