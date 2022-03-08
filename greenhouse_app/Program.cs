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

var builder = new ConfigurationBuilder();

builder.SetBasePath(Directory.GetCurrentDirectory());

builder.AddJsonFile("appsettings.json");

var config = builder.Build();

string connectionString = config.GetConnectionString("Mongo");

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<IParserProgramStages, ParserProgramStages>();
serviceCollection.AddTransient<IRepository<LoadedProgramBase>, MongoLoadedProgramRepository>(x => new MongoLoadedProgramRepository(connectionString));
serviceCollection.AddSingleton<ArduinoChannel>();
serviceCollection.AddSingleton<RaspberryChannel>();
serviceCollection.AddSingleton<ICommunicator, CommunicatorDevices>();
serviceCollection.AddTransient<Dispatcher>();
var serviceProvider = serviceCollection.BuildServiceProvider();

Console.WriteLine("Control application started");

var programFromFile = await new InDbTransmitterProgram<string, LoadedProgramBase>(
    new FromFileTransmitterProgram<string, LoadedProgramBase>(serviceProvider.GetService<IParserProgramStages>()),
    serviceProvider.GetService<IRepository<LoadedProgramBase>>()).TransmitProgram("ProgramExample.json");

//var mediator = new CommunicatorDevices();
//var arduino = new ArduinoChannel(mediator);
//var raspberry = new RaspberryChannel(mediator);
//mediator._arduinoChannel = arduino;
//mediator._raspberryChannel = raspberry;

//arduino.SendCommand("Hello I'am arduino uno!");

//raspberry.SendCommand("Hello I'm raspberry!");


//var dispatcher = serviceProvider.GetService<Dispatcher>();

var portDetected = await Configure();

//portDetected.ListenArduinoAsync(arduino);

var dispatcher = new Dispatcher(); //todo: Убрать констр по умолчанию и зарегистрировать красиво эту зависимоть




//dispatcher.ShowProgramMongoAsync();

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
