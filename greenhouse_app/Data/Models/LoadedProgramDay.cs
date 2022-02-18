using System;
using MongoDB.Bson.Serialization.Attributes;

namespace greenhouse_app.Data.Models
{
    public class LoadedProgramDay : ICloneable
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Date { get; set; }

        public int Ligth { get; set; }

        public int Water { get; set; }

        public int MinTemperature { get; set; }

        public int MaxTemperature { get; set; }

        public LoadedProgramDay() { }

        public LoadedProgramDay(LoadedProgramDay loadedProgramDay)
        {
            Date = loadedProgramDay.Date.AddDays(1);
            Ligth = loadedProgramDay.Ligth;
            Water = loadedProgramDay.Water;
            MinTemperature = loadedProgramDay.MinTemperature;
            MaxTemperature = loadedProgramDay.MaxTemperature;
        }

        public object Clone()
        {
            return new LoadedProgramDay(this);
        }
    }
}