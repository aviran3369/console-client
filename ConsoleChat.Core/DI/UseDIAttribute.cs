using Microsoft.Extensions.DependencyInjection;

namespace ConsoleChat.Core.DI;

[AttributeUsage(AttributeTargets.Class)]
public class UseDIAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; private set; }
    public List<Type> Implementings { get; } = new List<Type>();

    public UseDIAttribute(ServiceLifetime lifetime, Type implementing)
    {
        Init(lifetime, new Type[] { implementing });
    }

    public UseDIAttribute(ServiceLifetime lifetime, params Type[] implementings)
    {
        Init(lifetime, implementings);
    }

    private void Init(ServiceLifetime lifetime, IEnumerable<Type> implementings)
    {
        Lifetime = lifetime;
        Implementings.AddRange(implementings);
    }
}

