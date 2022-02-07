using System;
namespace greenhouse_app.Data.Models
{
    public class LoadedProgramStage
    {
        public string Name { get; set; }
        public int Days { get; set; }
        public int LightHours { get; set; }
        public int WaterPercent { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }

        public List<LoadedProgramDay> DaysCollection { get; set; } = new List<LoadedProgramDay>();
    }
}


