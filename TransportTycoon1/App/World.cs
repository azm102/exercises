using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class World
    {
        private readonly IReadOnlyList<Warehouse> _warehouses;
        private readonly IReadOnlyList<Transport> _transports;
        private readonly Map _map;

        public World(string input)
        {
            var factory = new Warehouse("Factory");
            var port = new Warehouse("Port");
            var warehouseA = new Warehouse("A");
            var warehouseB = new Warehouse("B");

            var factoryCargos = input.Split(",").Select(x =>
                {
                    if (x.Equals("A"))
                    {
                        return new Cargo { Location = factory, Destination = warehouseA };
                    }
                    else
                    {
                        return new Cargo { Location = factory, Destination = warehouseB };
                    }
                })
                .ToList();
            factory.Cargos.AddRange(factoryCargos);

            _map = new Map(new[]
            {
                new Map.Item(factory, warehouseA, new[] { new Road(factory, port, TransportType.Truck, 1), new Road(port, warehouseA, TransportType.Ship, 4) }),
                new Map.Item(factory, port, new[] { new Road(factory, port, TransportType.Truck, 1) }),
                new Map.Item(port, warehouseA, new[] { new Road(port, warehouseA, TransportType.Ship, 4) }),
                new Map.Item(factory, warehouseB, new[] { new Road(factory, warehouseB, TransportType.Truck, 5) })
            });

            _transports = new[]
            {
                new Transport("Truck 1", TransportType.Truck) { Track = new Track(factory, factory, 0) },
                new Transport("Truck 2", TransportType.Truck) { Track = new Track(factory, factory, 0) },
                new Transport("Ship 1", TransportType.Ship) { Track = new Track(port, port, 0) }
            };

            _warehouses = new[] { factory, port, warehouseA, warehouseB };
        }

        public int Solve()
        {
            var totalTime = 0;

            while (true)
            {
                var freeTransports = GetFreeTransports();
                freeTransports.ForEach(TakeCargoFromWarehouse);

                UpdateTransports();

                totalTime += 1;

                if (IsCompleted())
                {
                    break;
                }
            }

            return totalTime;
        }

        private IReadOnlyList<Transport> GetFreeTransports()
        {
            var transports = _transports
                .Where(x => x.Track.IsCompleted)
                .ToList();
            return transports;
        }

        private void TakeCargoFromWarehouse(Transport transport)
        {
            var (cargo, road) = GetOptimalCargoForTransport(transport);
            if (cargo == null)
            {
                return;
            }
            PutCargoToTransport(transport, cargo, road);
        }

        private (Cargo, Road) GetOptimalCargoForTransport(Transport transport)
        {
            // v1: take first element in warehouse queue
            var warehouse = transport.Track.To;
            var optimal = warehouse.Cargos
                .Where(x => x.Destination != warehouse)
                .Select(x =>
                {
                    var roads = _map.GetPathFromTo(warehouse, x.Destination);
                    var acceptable = roads.Any() && roads.First().Type == transport.Type;
                    return new { Acceptable = acceptable, Cargo = x, Roads = roads };
                })
                .FirstOrDefault(x => x.Acceptable);

            if (optimal != null)
            {
                return (optimal.Cargo, optimal.Roads.First());
            }
            return (null, null);
        }

        private void PutCargoToTransport(Transport transport, Cargo cargo, Road road)
        {
            var warehouse = transport.Track.To;
            var track = new Track(road.From, road.To, road.Time, cargo);
            transport.Track = track;
            warehouse.Cargos.Remove(cargo);
        }

        private void UpdateTransports()
        {
            _transports.ForEach(x => x.Move());

            _transports.ForEach(x =>
            {
                if (x.Track.IsCompleted)
                {
                    var destination = x.Track.To;
                    var cargo = x.Track.Cargo;
                    if (cargo != null)
                    {
                        cargo.Location = destination;
                        destination.Cargos.Add(cargo);
                        x.Track = new Track(x.Track.To, x.Track.From, x.Track.Time);
                    }
                }
            });
        }

        private bool IsCompleted()
        {
            var allCargosDelivered = _warehouses.All(x => x.Cargos.All(y => y.Destination == x));
            var transportsIsEmpty = !_transports.Any(x => x.Track?.Cargo != null);
            return allCargosDelivered && transportsIsEmpty;
        }
    }
}