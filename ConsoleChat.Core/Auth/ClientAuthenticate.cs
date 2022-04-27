using System.Net.Security;
using System.Net.Sockets;

namespace ConsoleChat.Core.Auth;

public class ClientAuthenticate : IAuthenticate
{
    public Guid? Authenticate(TcpClient client)
    {
        var clientStream = client.GetStream();
        NegotiateStream authStream = new NegotiateStream(clientStream, false);

        authStream.AuthenticateAsClient();

        byte[] clientId = Guid.NewGuid().ToByteArray();
        authStream.Write(clientId, 0, clientId.Length);

        Console.WriteLine("client authenticated: {0}", authStream.IsAuthenticated);

        return authStream.IsAuthenticated ? Guid.NewGuid() : null;
    }
}

