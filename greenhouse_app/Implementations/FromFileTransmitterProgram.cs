using System;
using greenhouse_app.Data.Models;
using greenhouse_app.Interfaces;
using Newtonsoft.Json;

namespace greenhouse_app.ProgramLogic
{
	public class FromFileTransmitterProgram<T, TResult> : TransmitterProgramBase<T, TResult>
	{
        private readonly IParserProgramStages _parserStages;

        public FromFileTransmitterProgram(IParserProgramStages parserStages)  : base()
		{
            _parserStages = parserStages;
        }

        public override async Task<TResult> TransmitProgram(T path)
        {
            var pathToFile = path as string ?? throw new NullReferenceException("Path was null reference or had other type");
            
            using (var readerFile = new StreamReader(pathToFile))
            {
                var textProgram = await readerFile.ReadToEndAsync();
                var programFromFile = JsonConvert.DeserializeObject<LoadedProgramBase>(textProgram);
                programFromFile.Stages = _parserStages.ParseProgramStages(programFromFile.Stages);
                return (TResult)(object)programFromFile;
            }
        }
    }
}

