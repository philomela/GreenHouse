using System;
namespace greenhouse_app.Data.DTOs
{
	public class MessageChannel
	{
        public DateTime DateMessage { get; set; }

        public int Ligth { get; set; }

        public int Water { get; set; }

        public int MinTemperature { get; set; }

        public int MaxTemperature { get; set; }
    }
}

