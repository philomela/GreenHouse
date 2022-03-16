using System;
using System.IO.Ports;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;
using MediatR;

namespace greenhouse_app.Extensions
{
    public static class SerialPortExtension
    {
        public static async Task ArduinoChannelWorkAsync(this SerialPort serialPort, IMediator mediator)
        {
            //serialPort.Open();

            await Task.Run(() =>
            {
                while (true)
                {
                    var msg = serialPort.ReadLine();
                    if (!string.IsNullOrEmpty(msg))
                        mediator.Send(new ExecuteRaspberryCommand(msg));
                }
            });

        }
    }
}

