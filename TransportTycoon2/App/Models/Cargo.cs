namespace TransportTycoon.Models
{
    public class Cargo
    {
        public Cargo(long id, Warehouse origin, Warehouse destination)
        {
            Id = id;
            Origin = origin;
            Destination = destination;
        }

        public long Id { get; }
        public Warehouse Origin { get; }
        public Warehouse Destination { get; }
    }
}