using ConsoleChat.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace ConsoleChat.Client.Handlers;

[UseDI(ServiceLifetime.Transient, typeof(IClientWriter))]
public class ClientWriter : IClientWriter
{
    public void Write(TcpClient client)
    {
        try
        {
            string str;
            SocketShutdown reason = SocketShutdown.Send;

            Console.Write("  >>>>: ");

            while (true)
            {
                str = Console.ReadLine();

                if (str == null) break;

                lock (ServerStatus.Lock)
                {
                    BinaryWriter writer = new BinaryWriter(client.GetStream());
                    writer.Write(str);

                    if (ServerStatus.Closed)
                    {
                        // Remote endpoint already said they are done sending,
                        // so we're done with both sending and receiving.
                        reason = SocketShutdown.Both;
                        break;
                    }
                }
            }

            client.Client.Shutdown(reason);
        }
        catch (IOException e)
        {
            Console.WriteLine("\nthe connection with the server has lost");
        }
    }
}

