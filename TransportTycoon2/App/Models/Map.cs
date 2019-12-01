using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon.Models
{
    public class Map
    {
        private readonly IReadOnlyList<Item> _items;

        public Map(IReadOnlyList<Item> items)
        {
            _items = items;
        }

        public IReadOnlyList<Road> GetPathFromTo(Warehouse from, Warehouse to)
        {
            if (from == to)
            {
                return new List<Road> { new Road(from, to, TransportType.Truck, 0) };
            }

            var item = _items.FirstOrDefault(x => x.From == from && x.To == to);
            if (item != null)
            {
                return item.Roads;
            }

            item = _items.FirstOrDefault(x => x.To == from && x.From == to);
            if (item != null)
            {
                return item.Roads.Select(x => new Road(x.To, x.From, x.Type, x.Time)).Reverse().ToList();
            }

            return new List<Road>();
        }

        public class Item
        {
            public Warehouse From { get; }
            public Item(Warehouse from, Warehouse to, IReadOnlyList<Road> roads)
            {
                From = from;
                To = to;
                Roads = roads;
            }

            public Warehouse To { get; }
            public IReadOnlyList<Road> Roads { get; }
        }
    }
}