using System;
using MediatR;

namespace greenhouse_app.Implementations
{
    public class ExecuteRaspberryCommand : IRequest<string>
    {
        public ExecuteRaspberryCommand(string message) => Message = message;

        public string Message { get; set; }
    }
}

