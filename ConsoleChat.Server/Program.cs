using ConsoleChat.Server;

var app = new ServerApplication();
app.ConfigureServices();
ServerApplication.Services = app.GetServices();
app.Run();