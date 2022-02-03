using System;
namespace greenhouse_app.ProgramLogic
{
	public class InDbTransmitterProgram : TransmitterProgramDecorator<string, string>
	{
		public InDbTransmitterProgram(TransmitterProgramBase<string, string> trans)
            : base(trans)
		{
		}
        
        public override string LoadProgram(string path)
        {
            return transmitter.LoadProgram(path) + "Functional load from db";
            
        }
    }
}

