using System.IO;

namespace TransportTycoon.Loggers
{
    public class FileEventLogger : IEventLogger
    {
        private readonly string _filename;

        public FileEventLogger(string filename)
        {
            _filename = filename + ".log";
            File.Create(_filename).Dispose();
        }

        public void Log(IEvent ev)
        {
            using (var writer = new StreamWriter(File.Open(_filename, FileMode.Append)))
            {
                writer.WriteLine(ev.ToString());
            }
        }
    }
}