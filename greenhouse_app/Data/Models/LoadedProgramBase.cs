using System;
namespace greenhouse_app.Data.Models
{
    public class LoadedProgramBase
    {
        public string NameProgram { get; set; }

        public LoadedProgramStage[] Stages { get; set; }
    }
}
