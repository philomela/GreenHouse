using System;
using greenhouse_app.Data.Models;
using greenhouse_app.Implementations;
using greenhouse_app.Interfaces;
using Newtonsoft.Json;

namespace greenhouse_app.Implementations
{
	public class InDbTransmitterProgram<T, TResult> : TransmitterProgramDecorator<T, TResult>
	{
        private readonly IRepository<LoadedProgramBase> _mongoRepository;
		public InDbTransmitterProgram(TransmitterProgramBase<T, TResult> trans, IRepository<LoadedProgramBase> mongoRepository)
            : base(trans)
		{
            _mongoRepository = mongoRepository;
        }
        
        public override async Task<TResult> TransmitProgram(T path)
        {
            var programDecorate = await transmitter.TransmitProgram(path) as LoadedProgramBase;
            _mongoRepository.Create(programDecorate);
            return (TResult)(object)programDecorate;           
        }
    }
}