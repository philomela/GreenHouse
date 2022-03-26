using System;
using MediatR;

namespace greenhouse_app.Implementations
{
	public class ExecuteArduinoCommandHandler : IRequestHandler<ExecuteArduinoCommand, string>
	{
		public ExecuteArduinoCommandHandler()
		{
		}

        public async Task<string> Handle(ExecuteArduinoCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(request.Message);
            });

            return null;
        }
    }
}

