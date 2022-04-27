using System.Net.Sockets;

namespace ConsoleChat.Client.Handlers;

public interface IClientWriter
{
    void Write(TcpClient client);
}

