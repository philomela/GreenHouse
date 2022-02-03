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

Console.WriteLine("Control application started");
Console.WriteLine(new InDbTransmitterProgram(new FromFileTransmitterProgram<string, string>()).LoadProgram(""));

var dispatcher = serviceProvider.GetService<Dispatcher>();

var portDetected = await Configure();





dispatcher.ShowProgramMongoAsync();

Console.ReadLine();

static async Task<string> Configure()
{
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

    string portDetected = await Task.Run(() =>
    {
        string portDetected = String.Empty;
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
    return portDetected;
}

static async Task<string> PingPort(SerialPort serialPort, CancellationToken token, int baudRate = 9600)
{
    var msgPing = string.Empty;

    serialPort.BaudRate = baudRate;
    serialPort.Open();

    while (msgPing != "PingMsg\r")
    {
        if (token.IsCancellationRequested)
            return String.Empty;
        msgPing = serialPort.ReadLine();
    }

    Console.WriteLine($"Port arduino detected: {serialPort.PortName}");
    await Task.Delay(2000);
    serialPort.Write(new byte[] { 1 }, 0, 1);
    Console.WriteLine("Arduino ready");

    return serialPort.PortName;
}
