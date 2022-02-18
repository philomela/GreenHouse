using System;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
	public class RaspberryChannel : ChannelBase
	{
		public RaspberryChannel(ICommunicator communicator) : base(communicator)
		{
		}

		public override void SendCommand(string message)
		{
			_communicator.Notify(this, message);
		}

		public override void GetCommand(string message)
        {
            Console.WriteLine($"Raspberry: {message}");
        }
	}
}

