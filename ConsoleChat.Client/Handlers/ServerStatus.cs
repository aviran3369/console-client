namespace ConsoleChat.Client.Handlers;

public static class ServerStatus
{
    public static object Lock { get; } = new object();
    public static bool Closed { get; set; } = false;

}

