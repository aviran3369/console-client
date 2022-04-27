using System.Net.Security;
using System.Net.Sockets;

namespace ConsoleChat.Core.Auth;

public class ServerAuthenticate : IAuthenticate
{
    public Guid? Authenticate(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            NegotiateStream authStream = new NegotiateStream(stream, false);
            //ClientState cState = new ClientState(authStream, clientSocket);
            authStream.AuthenticateAsServer();

            var clientIdBuffer = new byte[2048];
            var bytes = authStream.Read(clientIdBuffer, 0, clientIdBuffer.Length);
            var clientId = new Guid(clientIdBuffer.Take(bytes).ToArray());
            Console.WriteLine($"client authenticated with id: {clientId}");

            return clientId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine($"can't authenticate client");
        return null;
    }
}

