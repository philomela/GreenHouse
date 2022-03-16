using System;
using greenhouse_app.Data.Models;
using greenhouse_app.Interfaces;

namespace greenhouse_app.Implementations
{
    public class ParserProgramStages : IParserProgramStages
    {
        public LoadedProgramStage[] ParseProgramStages(LoadedProgramStage[] stages)
        {
            var lastDate = DateTime.Now.Date;
            foreach (var currStage in stages)
            {
                currStage.DaysCollection = new List<LoadedProgramDay>();
                currStage.DaysCollection.Add(new LoadedProgramDay()
                {
                    Ligth = currStage.LightHours,
                    Water = currStage.WaterPercent,
                    MinTemperature = currStage.MinTemperature,
                    MaxTemperature = currStage.MaxTemperature,
                    Date = lastDate
                }); ;

                for (int i = 0; i <= currStage.Days; i++)
                {
                    currStage.DaysCollection.Add(currStage.DaysCollection.LastOrDefault().Clone() as LoadedProgramDay);
                    if (i == currStage.Days - 1)
                        lastDate = lastDate.AddDays(currStage.Days -1);
                }
            }
            return stages;
        }
    }
}