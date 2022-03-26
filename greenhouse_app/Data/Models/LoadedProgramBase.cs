using System;
using MongoDB.Bson;
namespace greenhouse_app.Data.Models
{
    public class LoadedProgramBase
    {
        public ObjectId Id { get; set; }

        public string NameProgram { get; set; }

        public LoadedProgramStage[] Stages { get; set; }
    }
}
