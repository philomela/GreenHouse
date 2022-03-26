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
using greenhouse_app.Data.Models;
using MediatR;
using Newtonsoft.Json;

var builder = new ConfigurationBuilder();

builder.SetBasePath(Directory.GetCurrentDirectory());

builder.AddJsonFile("appsettings.json");

var config = builder.Build();

string connectionString = config.GetConnectionString("Mongo");

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<IParserProgramStages, ParserProgramStages>();
serviceCollection.AddTransient<IRepository<LoadedProgramBase>, MongoLoadedProgramRepository>(x => new MongoLoadedProgramRepository(connectionString));
serviceCollection.AddMediatR(typeof(Program));
 
serviceCollection.AddSingleton<TransmitterProgramBase<string, LoadedProgramBase>, FromFileTransmitterProgram<string, LoadedProgramBase>>();
serviceCollection.AddSingleton<TransmitterProgramDecorator<string, LoadedProgramBase>, InDbTransmitterProgram<string, LoadedProgramBase>>();

serviceCollection.AddTransient<Dispatcher>();
var serviceProvider = serviceCollection.BuildServiceProvider();

Console.WriteLine("Control application started");

var service = serviceProvider.GetRequiredService<TransmitterProgramDecorator<string, LoadedProgramBase>>();
service.TransmitProgram("ProgramExample.json");

await serviceProvider.GetService<Dispatcher>().RunProgram(null);

var portDetected = await Configure();

await serviceProvider.GetService<Dispatcher>().RunProgram(portDetected);

Console.WriteLine("Channels started");
Console.ReadLine();

static async Task<SerialPort> Configure()
{
    var serialPorts = SerialPort.GetPortNames();

    var taskSlice = new List<Task<SerialPort>>();

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

    var portDetected = await Task.Run(() =>
    {
        SerialPort portDetected = null;
        while (true)
        {
            portDetected = taskSlice.Where(
                x => x is not null
                && x.IsCompletedSuccessfully == true)?.FirstOrDefault()?.Result;

            if (portDetected is not null)
            {
                return portDetected;
            }
        }
    });

    cts.Cancel();
    return portDetected;
}

static async Task<SerialPort> PingPort(SerialPort serialPort, CancellationToken token, int baudRate = 9600)
{
    var msgPing = string.Empty;

    serialPort.BaudRate = baudRate;
    serialPort.Open();

    while (msgPing != "PingMsg\r")
    {
        if (token.IsCancellationRequested)
            return serialPort;
        msgPing = serialPort.ReadLine();
    }

    Console.WriteLine($"Port arduino detected: {serialPort.PortName}");
    await Task.Delay(2000);
    serialPort.Write(new byte[] { 1 }, 0, 1);
    Console.WriteLine("Arduino ready");

    return serialPort;
}
