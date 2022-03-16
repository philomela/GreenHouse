using System;
using System.IO.Ports;
using greenhouse_app.Data;
using greenhouse_app.Data.Models;
using greenhouse_app.Extensions;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace greenhouse_app.ProgramLogic
{
    /// <summary>
    /// Реализовать логику работы с каналом распберри
    /// </summary>
    public class Dispatcher  
    {
        private readonly IRepository<LoadedProgramBase> _mongoRepository;
        private readonly IMediator _mediator;
        private readonly ArduinoChannel _arduino;
        private readonly RaspberryChannel _raspberry;

        public Dispatcher()
        {}

        public Dispatcher(IRepository<LoadedProgramBase> mongoRepository, IMediator mediator) =>
            (_mongoRepository, _mediator) = (mongoRepository, mediator);

        public async Task RunProgram(SerialPort serialPort)
        {
            await RunArduino(serialPort);
            await RunRaspberry();
        }

        private async Task RunArduino(SerialPort serialPort) => await serialPort.ArduinoChannelWorkAsync(_mediator);

        private async Task RunRaspberry()
        {
            await Task.Run(() =>
            {
                var currentDate = DateTime.Now.Date;

                while (true)
                {
                    if (currentDate != DateTime.Now.Date)
                    {
                        currentDate = DateTime.Now.Date;

                        var currentDay = GetCurrentDayInProgram(currentDate);

                        //_raspberry.SendCommand($"ProgramDay={JsonConvert.SerializeObject(currentDay)}");
                    }
                }
            });          
        }

        private async Task<LoadedProgramDay> GetCurrentDayInProgram(DateTime currentDate)
        {
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
    }
}

