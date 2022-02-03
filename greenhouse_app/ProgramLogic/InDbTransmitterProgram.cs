using System;
using greenhouse_app.Data.Models;
using Newtonsoft.Json;

namespace greenhouse_app.ProgramLogic
{
	public class InDbTransmitterProgram<T, TResult> : TransmitterProgramDecorator<T, TResult>
	{
		public InDbTransmitterProgram(TransmitterProgramBase<T, TResult> trans)
            : base(trans)
		{
		}
        
        public override async Task<TResult> TransmitProgram(T path)
        {
            var programDecorate = await transmitter.TransmitProgram(path) as LoadedProgram;

            programDecorate.Days += 30;
            programDecorate.CollectionProgram.Add(new LoadedProgram() { Temperature = 100});

            return (TResult)(object)programDecorate;           
        }
    }
}

