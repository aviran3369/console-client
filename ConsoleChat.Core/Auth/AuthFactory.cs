
namespace ConsoleChat.Core.Auth;

public class AuthFactory
{
    public IAuthenticate CreateClientAuthenticate() => new ClientAuthenticate();
    public IAuthenticate CreateServerAuthenticate() => new ServerAuthenticate();
    public IAuthenticate Create<T>() where T : class, IAuthenticate
    {
        return (T)Activator.CreateInstance(typeof(T));
    }
}

