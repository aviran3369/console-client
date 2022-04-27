using System.Net.Sockets;

namespace ConsoleChat.Core.Auth;

public interface IAuthenticate
{
    Guid? Authenticate(TcpClient client);
}

