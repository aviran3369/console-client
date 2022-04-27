using ConsoleChat.Client.Handlers;
using ConsoleChat.Core.Auth;
using ConsoleChat.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace ConsoleChat.Client.Driver;

[UseDI(ServiceLifetime.Transient, typeof(IClientDriver))]
public class ClientDriver : IClientDriver
{
    private readonly IClientReader _clientReader;
    private readonly IClientWriter _clientWriter;
    private readonly IAuthenticate _auth;

    public ClientDriver(IClientReader clientReader, IClientWriter clientWriter)
    {
        _clientReader = clientReader;
        _clientWriter = clientWriter;

        var authFactory = new AuthFactory();
        _auth = authFactory.CreateClientAuthenticate();
    }

    public void Start(string host, int port)
    {
        TcpClient client = new TcpClient(host, port);

        
        if (AuthenticateClient(client))
        {
            Thread writeThread = new(() => _clientWriter.Write(client));
            Thread readThread = new(() => _clientReader.Read(client));

            writeThread.Start();
            readThread.Start();

            writeThread.Join();
            readThread.Join();
        }

        client.Close();
        Console.WriteLine("client exiting");
    }

    private bool AuthenticateClient(TcpClient client)
    {
        var clientId = _auth.Authenticate(client);
        return clientId.HasValue;
    }
}

