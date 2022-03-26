using System;
using MediatR;

namespace greenhouse_app.Implementations
{
	public class ExecuteArduinoCommand : IRequest<string>
	{
		public ExecuteArduinoCommand(string message) => Message = message; 

        public string Message { get; set; }
    }
}

