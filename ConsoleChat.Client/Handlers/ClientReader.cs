using ConsoleChat.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace ConsoleChat.Client.Handlers;

[UseDI(ServiceLifetime.Transient, typeof(IClientReader))]
public class ClientReader : IClientReader
{
    public void Read(TcpClient client)
    {
        try
        {
            while (true)
            {
                try
                {
                    BinaryReader reader = new BinaryReader(client.GetStream());
                    Console.WriteLine($"  <<<<: {reader.ReadString()}\n");
                    Console.Write($"  >>>>: ");
                }
                catch (EndOfStreamException)
                {
                    lock (ServerStatus.Lock)
                    {
                        ServerStatus.Closed = true;
                        return;
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("\nthe connection with the server has lost");
        }
    }
}

