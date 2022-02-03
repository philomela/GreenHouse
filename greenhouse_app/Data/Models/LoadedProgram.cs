using System;
namespace greenhouse_app.Data.Models
{
    public class LoadedProgram
    {
        public int ObjectId { get; set; }
        public string NameProgram { get; set; }
        public string NameStage { get; set; }
        public int Days { get; set; }
        public int Light { get; set; }
        public int Water { get; set; }
        public int Temperature { get; set; }

        public LoadedProgram()
        {
            CollectionProgram = new List<LoadedProgram>();
        }

        public List<LoadedProgram> CollectionProgram { get; protected set; }
    }
}
