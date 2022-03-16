using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
    public class ArduinoChannel : ChannelBase
    {
        public ArduinoChannel(ICommunicator communicator) : base(communicator)
        {}

        public async override void SendCommand(string message) => await _communicator.Notify(this, message);

        public override void GetCommand(string message) => Console.WriteLine(message);
    }
}