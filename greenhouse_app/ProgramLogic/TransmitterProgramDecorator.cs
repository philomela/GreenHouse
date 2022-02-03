using System;
namespace greenhouse_app.ProgramLogic
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

