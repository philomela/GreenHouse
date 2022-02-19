using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
    public class ArduinoChannel : ChannelBase<string>
    {
        protected override Queue<string> Messages { get; set; } = new Queue<string>();

        public ArduinoChannel(ICommunicator communicator) : base(communicator)
        {
        }

        public async override void SendCommand(string message) => await _communicator.Notify(this, message);

        public override void GetCommand(string message) => Messages.Enqueue(message);
    }
}