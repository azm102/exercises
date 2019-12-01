using System.Collections.Generic;

namespace TransportTycoon.Loggers
{
    public class MemoryEventLogger : IEventLogger
    {
        public List<string> Logs { get; } = new List<string>();

        public void Log(IEvent ev)
        {
            Logs.Add(ev.ToString());
        }
    }
}