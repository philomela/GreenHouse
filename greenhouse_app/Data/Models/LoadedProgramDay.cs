using System;
namespace greenhouse_app.Data.Models
{
    public class LoadedProgramDay : ICloneable
    {
        public int Ligth { get; set; }
        public int Water { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }

        public LoadedProgramDay() { }

        public LoadedProgramDay(int light, int water, int minTemperature, int maxTemperature)
        {
            Ligth = light;
            Water = water;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
        }

        public object Clone()
        {
            return new LoadedProgramDay(Ligth, Water, MinTemperature, MaxTemperature);
        }
    }
}