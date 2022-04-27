using System.Net.Sockets;

namespace ConsoleChat.Client.Handlers;

public interface IClientReader
{
    void Read(TcpClient client);
}

