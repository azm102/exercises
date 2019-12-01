namespace TransportTycoon.Models
{
    public class Road
    {
        public Road(Warehouse from, Warehouse to, TransportType type, int time)
        {
            From = from;
            To = to;
            Type = type;
            Time = time;
        }
        public TransportType Type { get; }
        public int Time { get; }
        public Warehouse From { get; }
        public Warehouse To { get; }
    }
}