using System;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
	public class RaspberryChannel : ChannelBase
	{
		public RaspberryChannel(ICommunicator communicator) : base(communicator)
		{}

		public async override void SendCommand(string message) => await _communicator.Notify(this, message);

		public override void GetCommand(string message) => Console.WriteLine(message);
	}
}

