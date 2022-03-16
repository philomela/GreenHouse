namespace greenhouse_app.Interfaces;

abstract public class ChannelBase
{   
    protected ICommunicator _communicator;

    //protected abstract event Func<string, string> ResultCommand

    public ChannelBase(ICommunicator communicator) 
    {
        _communicator = communicator;
    }

    public abstract void SendCommand(string message);
    public abstract void GetCommand(string message);

}