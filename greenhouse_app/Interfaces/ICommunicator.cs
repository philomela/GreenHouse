using System;
namespace greenhouse_app.Interfaces
{
	public interface ICommunicator
	{
		Task Notify(object sender, string message);
	}
}

