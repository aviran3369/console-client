using ConsoleChat.Core.DI;
using ConsoleChat.Server.Client;
using ConsoleChat.Server.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;
using System.Text;

namespace ConsoleChat.Server.Handler;

[UseDI(ServiceLifetime.Transient, typeof(IClientHandler))]
public class ClientHandler : IClientHandler
{
    private ClientSocket _client;

    public void Handle(ClientSocket client)
    {
        _client = client;
        Thread clientTread = new Thread(HandleClient);
        clientTread.Start();
    }

    private void HandleClient()
    {
        BinaryReader reader = new BinaryReader(_client.Socket.GetStream());

        try
        {
            while (true)
            {
                string message = reader.ReadString();
                Console.WriteLine($"  >>>> message from {_client.Id}: {message}");
                message = message.ReverseString();

                BinaryWriter writer = new BinaryWriter(_client.Socket.GetStream());
                writer.Write(message);
            }
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine($"client {_client.Id} disconnecting: {_client.Socket.Client.RemoteEndPoint}");
            _client.Socket.Client.Shutdown(SocketShutdown.Both);
        }
        catch (IOException e)
        {
            Console.WriteLine($"the connection with client {_client.Id} has lost.");
        }

        _client.Socket.Close();
        _client.Disconnect();
    }
}
