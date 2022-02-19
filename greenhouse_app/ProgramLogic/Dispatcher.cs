using System;
using greenhouse_app.Data;
using greenhouse_app.Data.Models;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;

namespace greenhouse_app.ProgramLogic
{
    /// <summary>
    /// Реализовать логику работы с каналом распберри
    /// </summary>
    public class Dispatcher  
    {
        private readonly IAuditorable _auditor;
        private readonly IControlable _controller;
        private readonly MongoLoadedProgramRepository _mongoLoadedProgramRepository;

        public Dispatcher(IAuditorable auditor, IControlable controller, MongoLoadedProgramRepository mongoLoadedProgramRepository)
        {
            _auditor = auditor;
            _controller = controller;
            _mongoLoadedProgramRepository = mongoLoadedProgramRepository;
        }

        public async Task ShowProgramMongoAsync()
        {
            var listProgram = await _mongoLoadedProgramRepository.GetLoadedProgramListAsync();
            listProgram.ForEach(x => Console.WriteLine(x));
        }
    }
}

