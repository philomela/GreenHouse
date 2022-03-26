using System;
using greenhouse_app.Data.Models;

namespace greenhouse_app.Interfaces
{
	public interface IParserProgramStages
	{
		LoadedProgramStage[] ParseProgramStages(LoadedProgramStage[] stages);
	}
}

   