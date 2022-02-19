using System;
using System.IO.Ports;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
    public class CommunicatorDevices : ICommunicator
    {
        public ArduinoChannel _arduinoChannel { get; set; }
        public RaspberryChannel _raspberryChannel { get; set; }

        public async Task Notify(object sender, string message)
        {
            if (sender == _arduinoChannel)
            {
                _raspberryChannel.GetCommand(message);
                return;
            }
            _arduinoChannel.GetCommand(message);
        }
    }
}