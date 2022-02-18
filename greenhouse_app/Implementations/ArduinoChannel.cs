using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
    public class ArduinoChannel : ChannelBase
    {
        public ArduinoChannel(ICommunicator communicator) : base(communicator)
        {
        }

        public override void SendCommand(string message)
        {
            _communicator.Notify(this, message);
        }

        public override void GetCommand(string message)
        {
            Console.WriteLine($"ArduinoChannel: {message}");
        }
    }
}