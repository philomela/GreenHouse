using System;
using System.IO.Ports;

namespace greenhouse_app.Interfaces
{
	public interface IRaspberryManager
	{
		Task RunRaspberryAsync(SerialPort serialPort);
	}
}

