namespace TransportTycoon.Loggers
{
    public class EmptyEventLogger : IEventLogger
    {
        public void Log(IEvent ev)
        {

        }
    }
}