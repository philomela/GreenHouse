using System;
using System.IO.Ports;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
    public class CommunicatorDevices : ICommunicator
    {
        private readonly ArduinoChannel _arduinoChannel;
        private readonly RaspberryChannel _raspberryChannel;

        public CommunicatorDevices(ArduinoChannel arduinoChannel, RaspberryChannel raspberryChannel) =>
            (_arduinoChannel, _raspberryChannel) = (arduinoChannel, raspberryChannel);

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