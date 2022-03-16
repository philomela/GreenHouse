using System;
using MediatR;

namespace greenhouse_app.Implementations
{
    public class ExecuteRaspberryCommandHandler : IRequestHandler<ExecuteRaspberryCommand, string>
    {
        public ExecuteRaspberryCommandHandler()
        {
        }

        public async Task<string> Handle(ExecuteRaspberryCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Raspberry get work");
            });

            return null;
        }
    }
}

