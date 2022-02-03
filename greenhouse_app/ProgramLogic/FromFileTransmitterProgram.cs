using System;
using greenhouse_app.Data.Models;
using Newtonsoft.Json;

namespace greenhouse_app.ProgramLogic
{
	public class FromFileTransmitterProgram<T, TResult> : TransmitterProgramBase<T, TResult>
	{
		public FromFileTransmitterProgram()  : base()
		{
        }

        public override async Task<TResult> TransmitProgram(T path)
        {
            var pathToFile = path as string ?? throw new NullReferenceException("Path was null reference or had other type");
            
            using (var readerFile = new StreamReader(pathToFile))
            {
                var textProgram = await readerFile.ReadToEndAsync();
                var programFromFile = JsonConvert.DeserializeObject<LoadedProgram>(textProgram);

                return (TResult)(object)programFromFile;
            }
        }
    }
}

