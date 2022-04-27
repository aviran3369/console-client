using ConsoleChat.Client.Driver;
using ConsoleChat.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleChat.Client;

public class ClientApplication : ApplicationExecutor
{
    public static IServiceProvider Services { get; set; }

    public override void Run()
    {
        var clientProvider = Services.GetService<IClientDriver>();
        clientProvider.Start("localhost", 2022);
    }
}

