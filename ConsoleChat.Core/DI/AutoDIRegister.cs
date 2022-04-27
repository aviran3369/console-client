
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConsoleChat.Core.DI;

public static class AutoDIRegister
{
    public static void RegisterDIServices(IServiceCollection services)
    {
        var diServices = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes()
                .Where(t => t.IsDefined(typeof(UseDIAttribute)) && t.IsClass && !t.IsAbstract));
        

        foreach (var service in diServices)
        {
            var attr = service.GetCustomAttribute<UseDIAttribute>();

            if (attr == null || attr.Implementings == null || attr.Implementings.Count == 0) continue;
        

            foreach (var impl in attr.Implementings)
            {
                services.Add(new ServiceDescriptor(impl, service, attr.Lifetime));
            }

        }
    }
}

