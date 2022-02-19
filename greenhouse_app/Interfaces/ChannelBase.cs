namespace greenhouse_app.Interfaces;

abstract public class ChannelBase<T>
{   
    protected ICommunicator _communicator;

    protected abstract Queue<T> Messages { get; set; }

    public ChannelBase(ICommunicator communicator) 
    {
        _communicator = communicator;
    }

    public abstract void SendCommand(string message);
    public abstract void GetCommand(string message);

}