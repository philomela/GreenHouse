using System;
using greenhouse_app.Data.Models;
using greenhouse_app.Interfaces;

namespace greenhouse_app.ProgramLogic
{
	public abstract class TransmitterProgramBase<T, TResult>
	{
		private readonly IRepository<LoadedProgramBase> _repository;

		public abstract Task<TResult> TransmitProgram(T path);
	}
}
 
