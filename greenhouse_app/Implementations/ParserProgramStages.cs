using System;
using greenhouse_app.Data.Models;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
    public class ParserProgramStages : IParserProgramStages
    {
        public LoadedProgramStage[] ParseProgramStages(LoadedProgramStage[] stages)
        {
            foreach (var currStage in stages)
            {
                currStage.DaysCollection = new List<LoadedProgramDay>();
                currStage.DaysCollection.Add(new LoadedProgramDay()
                {
                    Ligth = currStage.LightHours,
                    Water = currStage.WaterPercent,
                    MinTemperature = currStage.MinTemperature,
                    MaxTemperature = currStage.MaxTemperature
                });

                for (int i = 0; i < currStage.Days - 1; i++)
                {
                    currStage.DaysCollection.Add(currStage.DaysCollection.LastOrDefault().Clone() as LoadedProgramDay);
                }
            }
            return stages;
        }
    }
}