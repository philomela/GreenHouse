using System;
namespace greenhouse_app.ProgramLogic
{
	public class FromFileTransmitterProgram<T, TResult> : TransmitterProgramBase<T, TResult> where TResult : class
	{
		public FromFileTransmitterProgram()  : base()
		{
        }

        public override TResult LoadProgram(T path)
        {
            return (TResult)(object)"Functional load from file";
        }
    }
}

