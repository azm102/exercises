using System;

namespace TransportTycoon.Loggers
{
    public class ConsoleEventLogger : IEventLogger
    {
        public void Log(IEvent ev) => Console.WriteLine(ev.ToString());
    }
}