namespace App
{
    public class Track
    {
        private int _currentTime;

        public Track(Warehouse from, Warehouse to, int time, Cargo cargo = null)
        {
            From = from;
            To = to;
            Cargo = cargo;
            Time = time;
        }

        public Warehouse From { get; }
        public Warehouse To { get; }
        public Cargo Cargo { get; }
        public int Time { get; }
        public bool IsCompleted => Time <= _currentTime;
        public void IncTime() => _currentTime += 1;
    }
}