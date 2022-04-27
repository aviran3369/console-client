using ConsoleChat.Core.Auth;
using ConsoleChat.Core.DI;
using ConsoleChat.Server.Client;
using ConsoleChat.Server.Handler;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace ConsoleChat.Server.Listener;

[UseDI(ServiceLifetime.Transient, typeof(IClientListener))]
public class ClientListener : IClientListener
{
    private readonly IAuthenticate _auth;

    public ClientListener()
    {
        var authFactory = new AuthFactory();
        _auth = authFactory.CreateServerAuthenticate();
    }   

    public void Listen(ListenerOptions options)
    {
        TcpListener ServerSocket = new(options.IPAddress, options.Port);
        ServerSocket.Start();

        Console.WriteLine("Server started.");
        
        ServerLoop(ServerSocket);
    }

    public void ServerLoop(TcpListener ServerSocket)
    {
        while (true)
        {
            TcpClient clientSocket = ServerSocket.AcceptTcpClient();
            Console.WriteLine($"client connected: {clientSocket.Client.RemoteEndPoint}");

            var clientId = _auth.Authenticate(clientSocket);

            if (!clientId.HasValue)
            {
                continue;
            }

            var client = new ClientSocket(clientId.Value, clientSocket);
            ClientStore.Instance.AddClient(client);

            var clientHandler = ServerApplication.Services.GetService<IClientHandler>();
            clientHandler.Handle(client);

            Console.WriteLine($"{ClientStore.Instance.GetClientCount()} clients connected");
        }
    }
}

