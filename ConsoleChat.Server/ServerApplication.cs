using ConsoleChat.Core;
using ConsoleChat.Server.Listener;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace ConsoleChat.Server;

public class ServerApplication : ApplicationExecutor
{
    public static IServiceProvider Services { get; set; }

    public override void Run()
    {
        var listener = _services.GetService<IClientListener>();

        // received from some configuration
        IPHostEntry hostname = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = hostname.AddressList[0];
        int port = 2022;

        if (listener == null)
        {
            Console.WriteLine("Can't find the listener service.");
            return;
        }

        var options = new ListenerOptions(ipAddress, port);
        listener.Listen(options);
    }
}

