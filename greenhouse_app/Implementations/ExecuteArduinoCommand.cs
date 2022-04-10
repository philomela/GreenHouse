using System;
using System.IO.Ports;
using MediatR;

namespace greenhouse_app.Implementations
{
	public class ExecuteArduinoCommand : IRequest<string>
	{
		public ExecuteArduinoCommand(string message, SerialPort serialPort) =>
			(Message, SerialPortArduino) = (message, serialPort);

		public string Message { get; set; }

		public SerialPort SerialPortArduino {get; set;}
    }
}

