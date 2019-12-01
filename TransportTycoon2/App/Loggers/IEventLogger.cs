namespace TransportTycoon.Loggers
{
    public interface IEventLogger
    {
        void Log(IEvent ev);        
    }
}