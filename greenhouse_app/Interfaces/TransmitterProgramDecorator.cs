using System;
using greenhouse_app.ProgramLogic;

namespace greenhouse_app.Interfaces
{
	public abstract class TransmitterProgramDecorator<T, TResult> : TransmitterProgramBase<T, TResult>
	{
		protected TransmitterProgramBase<T, TResult> transmitter;

		public TransmitterProgramDecorator(TransmitterProgramBase<T, TResult> transmitter)
		{
			this.transmitter = transmitter;
		}
	}
}

