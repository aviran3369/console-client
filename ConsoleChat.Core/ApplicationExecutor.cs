using ConsoleChat.Core.DI;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleChat.Core;

public abstract class ApplicationExecutor
{
    protected IServiceProvider _services;

    public ApplicationExecutor()
    {
    }

    public abstract void Run();

    protected virtual void CustomConfigureServices(IServiceCollection services)
    {
        // do nothing here
    }

    public IServiceProvider GetServices() => _services;

    public void ConfigureServices()
    {
        var services = new ServiceCollection();
        AutoDIRegister.RegisterDIServices(services);
        
        CustomConfigureServices(services);

        _services = services.BuildServiceProvider();
    }
}

