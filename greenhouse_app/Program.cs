using System.IO.Ports;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection();
serviceProvider.AddTransient<IAuditorable, AuditorGreenHouse>();
serviceProvider.AddTransient<IControlable, ControllerGreenHouse>();


Console.WriteLine("Control application started");

SerialPort serialPort = new SerialPort();
serialPort.PortName = "/dev/tty.usbmodem14101";
serialPort.BaudRate = 9600;
serialPort.Open();
while (true)
{
    string msg = serialPort.ReadExisting();
    Console.WriteLine(msg);
    Thread.Sleep(1000);    
}