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

        public Dispatcher()
        { }

        public Dispatcher(IRepository<LoadedProgramBase> mongoRepository, IMediator mediator) =>
            (_mongoRepository, _mediator) = (mongoRepository, mediator);

        public async Task RunProgram(SerialPort serialPort)
        {

            await RunRaspberry();
            await RunArduino(serialPort);
      
            //await Task.Run(() =>
            //{
            //    RunRaspberry();
            //    RunArduino(serialPort);
            //});

        }

        private async Task RunArduino(SerialPort serialPort) => await serialPort.ArduinoChannelWorkAsync(_mediator);

        private async Task RunRaspberry()
        {
            var currentDate = DateTime.Now.Date;

            var day = await GetCurrentDayInProgram(currentDate);

            var dayJson = JsonConvert.SerializeObject(day, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });

            _mediator.Send(new ExecuteArduinoCommand($"ProgramDay={dayJson}"));

            await Task.Run(() =>
            {
                

                while (true)
                {
                    if (currentDate != DateTime.Now.Date)
                    {
                        currentDate = DateTime.Now.Date;

                        var currentDay = GetCurrentDayInProgram(currentDate);

                        _mediator.Send(new ExecuteArduinoCommand($"ProgramDay={JsonConvert.SerializeObject(currentDay)}"));
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
                    if (day.Date.ToShortDateString() == currentDate.ToShortDateString())
                    {
                        return day;
                    }
                }
            }

            throw new Exception("Didn't difine day");
        }
    }
}

