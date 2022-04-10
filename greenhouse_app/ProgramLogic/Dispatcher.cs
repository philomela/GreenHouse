using System;
using System.IO.Ports;
using greenhouse_app.Data;
using greenhouse_app.Data.Models;
using greenhouse_app.Extensions;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace greenhouse_app.ProgramLogic
{
    public class Dispatcher
    {
        private readonly IArduinoManager<SerialPort> _arduinoManager;
        private readonly IRaspberryManager _raspberryManager;

        public Dispatcher()
        { }

        public Dispatcher(IArduinoManager<SerialPort> arduinoManager, IRaspberryManager raspberryManager) =>
            (_arduinoManager, _raspberryManager) = (arduinoManager, raspberryManager);

        public async Task RunProgram(SerialPort serialPort)
        {
            await _raspberryManager.RunRaspberryAsync(serialPort);
            await _arduinoManager.RunArduinoAsync(serialPort);
        }
    }
}

