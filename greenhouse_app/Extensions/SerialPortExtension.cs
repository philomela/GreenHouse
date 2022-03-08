using System;
using System.IO.Ports;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Extensions
{
    public static class SerialPortExtension
    {
        public static async void ListenArduinoAsync<T>(this SerialPort serialPort, ChannelBase<T> channel)
        {
            serialPort.Open();

            //while (true)
            //{
                var msg = serialPort.ReadLine();
                if (!string.IsNullOrEmpty(msg))
                    channel.SendCommand(msg);
            //}
        }
    }
}

