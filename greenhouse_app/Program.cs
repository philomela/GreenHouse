using System.IO.Ports;
using greenhouse_app.Data;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;
using greenhouse_app.ProgramLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Linq;
using greenhouse_app.Extensions;

var builder = new ConfigurationBuilder();

builder.SetBasePath(Directory.GetCurrentDirectory());

builder.AddJsonFile("appsettings.json");

var config = builder.Build();

string connectionString = config.GetConnectionString("Mongo");

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<IAuditorable, AuditorGreenHouse>();
serviceCollection.AddTransient<IControlable, ControllerGreenHouse>();
serviceCollection.AddTransient<MongoLoadedProgramRepository>(x => new MongoLoadedProgramRepository(connectionString));
serviceCollection.AddTransient<Dispatcher>();
var serviceProvider = serviceCollection.BuildServiceProvider();

var portDetect = await Configure(serviceProvider.GetService<Dispatcher>());

Console.WriteLine($"{portDetect}");

Console.ReadLine();

static async Task<string> Configure(Dispatcher dispatcher)
{
    Console.WriteLine("Control application started");

    dispatcher.ShowProgramMongoAsync();


    var serialPorts = SerialPort.GetPortNames();

    var taskSlice = new List<Task<string>>();

    var cts = new CancellationTokenSource();
    var token = cts.Token;

    foreach (var currNamePort in serialPorts)
    {
        taskSlice.Add(
                Task.Run(
                async () =>
                {
                    var currSer = new SerialPort(currNamePort);
                    return await PingPort(currSer, token);
                }));
    }

    string portDetect = await Task.Run(() =>
    {
        string portDetected = null;
        while (true)
        {
            portDetected = taskSlice.Where(
                x => x is not null
                && x.IsCompletedSuccessfully == true)?.FirstOrDefault()?.Result;

            if (!string.IsNullOrEmpty(portDetected))
            {
               return portDetected;
            }
        }
    });

    cts.Cancel();
    return portDetect;
}

static async Task<string> PingPort(SerialPort serialPort, CancellationToken token, int baudRate = 9600)
{
    var msgPing = string.Empty;
    //var serialPort = new SerialPort(namePort);

    serialPort.BaudRate = baudRate;
    serialPort.Open();

    while (msgPing != "PingMsg\r")
    {
        if (token.IsCancellationRequested)
            return null;
        msgPing = serialPort.ReadLine();     
    }

    Console.WriteLine($"Port arduino detected: {serialPort.PortName}");
    await Task.Delay(2000);
    serialPort.Write(new byte[] { 1 }, 0, 1);
    Console.WriteLine("Arduino ready");

    return serialPort.PortName;
}
