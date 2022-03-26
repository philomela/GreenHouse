using System;
using System.IO.Ports;
using greenhouse_app.Extensions;
using greenhouse_app.Interfaces;
using MediatR;

namespace greenhouse_app.Implementations
{
	public class ArduinoManager : IArduinoManager<SerialPort>
	{
        private readonly IMediator _mediator;

        public ArduinoManager(IMediator mediator) => _mediator = mediator;

        public async Task RunArduinoAsync(SerialPort serialPort)
        {
            await serialPort.ArduinoChannelWorkAsync(_mediator);
        }
    }
}

