using ConsoleChat.Server.Client;
using System.Net.Sockets;

namespace ConsoleChat.Server.Handler;

public interface IClientHandler
{
    void Handle(ClientSocket client);
}

