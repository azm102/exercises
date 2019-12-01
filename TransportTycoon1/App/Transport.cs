namespace App
{
    public class Transport
    {
        public Transport(string name, TransportType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public TransportType Type { get; }
        public Track Track { get; set; }

        public void Move()
        {
            Track?.IncTime();
        }

        public override string ToString() => $"{Type} {Name}";
    }
}