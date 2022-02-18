using System;
namespace greenhouse_app.Interfaces
{
	public interface ICommunicator
	{
		void Notify(object sender, string message);
	}
}

