using ConsoleChat.Client;

var app = new ClientApplication();
app.ConfigureServices();
ClientApplication.Services = app.GetServices();
app.Run();