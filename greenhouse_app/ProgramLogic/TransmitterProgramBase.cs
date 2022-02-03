using System;
using greenhouse_app.Data.Models;
using greenhouse_app.Interfaces;

namespace greenhouse_app.ProgramLogic
{
	public abstract class TransmitterProgramBase<T, TResult>
	{
		private readonly IRepository<LoadedProgram> _repository;

		public Func<T, TResult> baseAction { get;  set; }

		public abstract TResult LoadProgram(T path);
	}
}
 
