using System.Net;

namespace ConsoleChat.Server.Listener;

public record ListenerOptions(IPAddress IPAddress, int Port = 2022, int? MaxConnections = 10);
