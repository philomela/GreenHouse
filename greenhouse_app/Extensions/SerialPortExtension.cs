using System;
using System.IO.Ports;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Extensions
{
	public static class SerialPortExtension
	{
		public static void ListenArduino(this SerialPort serialPort, ChannelBase channel)
        {
			serialPort.Open();

			while (true)
            {
				var msg = serialPort.ReadLine();
				channel.SendCommand(msg);
            }
        }
	}
}

