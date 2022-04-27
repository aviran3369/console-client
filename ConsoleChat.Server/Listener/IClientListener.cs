using ConsoleChat.Server.Client;

namespace ConsoleChat.Server.Listener;

public interface IClientListener
{
    void Listen(ListenerOptions options);
}

