using System;
using MediatR;

namespace greenhouse_app.Implementations
{
	public class ExecuteArduinoCommand : IRequest<string>
	{
		public ExecuteArduinoCommand(string message) => message = Message; 

        public string Message { get; set; }
    }
}

