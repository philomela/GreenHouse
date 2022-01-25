using System;
namespace greenhouse_app.Interfaces
{
    public interface IControlable
    {
        void TurnOnLamps();

        void TurnOffLamps();

        void TurnOnTemperature();

        void TurnOffTemperature();

        void TurnOnFans();

        void TurnOffFans();

        void TurnOnWatering();

        void TurnOffWatering();
    }
}

