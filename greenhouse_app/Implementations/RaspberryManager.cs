using System;
using System.IO.Ports;
using greenhouse_app.Data.Models;
using greenhouse_app.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace greenhouse_app.Implementations
{
	public class RaspberryManager : IRaspberryManager
	{
        private readonly IRepository<LoadedProgramBase> _mongoRepository;
        private readonly IMediator _mediator;

        public RaspberryManager(IMediator mediator, IRepository<LoadedProgramBase> mongoRepository) =>
            (_mediator, _mongoRepository) = (mediator, mongoRepository);

        public async Task RunRaspberryAsync(SerialPort serialPort)
        {
            var currentDate = DateTime.Now.Date;

            var day = await GetCurrentDayInProgram(currentDate);

            var dayJson = JsonConvert.SerializeObject(day, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });

            var result = _mediator.Send(new ExecuteArduinoCommand($"ProgramDay={dayJson}", serialPort));

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

