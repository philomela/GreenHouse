using System;
using System.IO.Ports;
using greenhouse_app.Data;
using greenhouse_app.Data.Models;
using greenhouse_app.Extensions;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;
using Newtonsoft.Json;

namespace greenhouse_app.ProgramLogic
{
    /// <summary>
    /// Реализовать логику работы с каналом распберри
    /// </summary>
    public class Dispatcher  
    {
        private readonly IRepository<LoadedProgramBase> _mongoRepository;
        private readonly ArduinoChannel _arduino;
        private readonly RaspberryChannel _raspberry;

        public Dispatcher()
        {}

        public Dispatcher(IRepository<LoadedProgramBase> mongoRepository, ArduinoChannel arduino, RaspberryChannel raspberry) =>
            (_mongoRepository, _arduino, _raspberry) = (mongoRepository, arduino, raspberry);

        public async Task RunProgram(SerialPort serialPort)
        {
            var currentDay = GetCurrentDayInProgram();

            _raspberry.SendCommand($"ProgramDay={JsonConvert.SerializeObject(currentDay)}");

            serialPort.ListenArduinoAsync(_arduino);

            while(true)
            {
                serialPort
            }


        }

        private async Task<LoadedProgramDay> GetCurrentDayInProgram()
        {
            var currentDate = DateTime.Now.Date;

            var programs = await _mongoRepository.GetLoadedProgramListAsync();

            var currentProgram = programs.Last();

            foreach (var currStage in currentProgram.Stages)
            {
                foreach (var day in currStage.DaysCollection)
                {
                    if (day.Date == currentDate)
                    {
                        return day;
                    }
                }
            }

            throw new Exception("Didn't difine day");
        }

        private async Task GetParametersDay(LoadedProgramDay currentDay) {
            
        }


        public async Task ShowProgramMongoAsync()
        {
            var listProgram = await _mongoRepository.GetLoadedProgramListAsync();
            listProgram.ForEach(x => Console.WriteLine(x));
        }
    }
}

