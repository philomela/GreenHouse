using System;
namespace greenhouse_app.Interfaces
{
	public interface IArduinoManager<T>
	{
		Task RunArduinoAsync(T serialPort);
	}
}

