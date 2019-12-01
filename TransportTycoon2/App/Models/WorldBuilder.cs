using System.Linq;
using TransportTycoon.Loggers;

namespace TransportTycoon.Models
{
    public static class WorldBuilder
    {
        public static World CreateForExercise1(string input, IEventLogger eventLogger)
        {
            var factory = new Warehouse("FACTORY");
            var port = new Warehouse("PORT");
            var warehouseA = new Warehouse("A");
            var warehouseB = new Warehouse("B");

            var factoryCargos = input.Split(",").Select((x, id) =>
                {
                    if (x.Equals("A"))
                    {
                        return new Cargo(id, factory, warehouseA);
                    }
                    else
                    {
                        return new Cargo(id, factory, warehouseB);
                    }
                })
                .ToList();
            factory.Cargos.AddRange(factoryCargos);

            var map = new Map(new[]
            {
                new Map.Item(factory, warehouseA, new[] { new Road(factory, port, TransportType.Truck, 1), new Road(port, warehouseA, TransportType.Ship, 4) }),
                new Map.Item(factory, port, new[] { new Road(factory, port, TransportType.Truck, 1) }),
                new Map.Item(port, warehouseA, new[] { new Road(port, warehouseA, TransportType.Ship, 4) }),
                new Map.Item(factory, warehouseB, new[] { new Road(factory, warehouseB, TransportType.Truck, 5) })
            });

            var transports = new[]
            {
                new Transport(id: 0, name: "Truck 1", location: factory, type: TransportType.Truck, capacity: 1, loadTime: 0, unloadTime: 0),
                new Transport(id: 1, name: "Truck 2", location: factory, type: TransportType.Truck, capacity: 1, loadTime: 0, unloadTime: 0),
                new Transport(id: 2, name: "Ship 1", location: port, type: TransportType.Ship, capacity: 1, loadTime: 0, unloadTime: 0),
            };

            var warehouses = new[] { factory, port, warehouseA, warehouseB };

            return new World(eventLogger, warehouses, transports, map);
        }

        public static World CreateForExercise2(string input, IEventLogger eventLogger)
        {
            var factory = new Warehouse("FACTORY");
            var port = new Warehouse("PORT");
            var warehouseA = new Warehouse("A");
            var warehouseB = new Warehouse("B");

            var factoryCargos = input.Split(",").Select((x, id) =>
                {
                    if (x.Equals("A"))
                    {
                        return new Cargo(id, factory, warehouseA);
                    }
                    else
                    {
                        return new Cargo(id, factory, warehouseB);
                    }
                })
                .ToList();
            factory.Cargos.AddRange(factoryCargos);

            var map = new Map(new[]
            {
                new Map.Item(factory, warehouseA, new[] { new Road(factory, port, TransportType.Truck, 1), new Road(port, warehouseA, TransportType.Ship, 6) }),
                new Map.Item(factory, port, new[] { new Road(factory, port, TransportType.Truck, 1) }),
                new Map.Item(port, warehouseA, new[] { new Road(port, warehouseA, TransportType.Ship, 6) }),
                new Map.Item(factory, warehouseB, new[] { new Road(factory, warehouseB, TransportType.Truck, 5) })
            });

            var transports = new[]
            {
                new Transport(id: 0, name: "Truck 1", location: factory, type: TransportType.Truck, capacity: 1, loadTime: 0, unloadTime: 0),
                new Transport(id: 1, name: "Truck 2", location: factory, type: TransportType.Truck, capacity: 1, loadTime: 0, unloadTime: 0),
                new Transport(id: 2, name: "Ship 1", location: port, type: TransportType.Ship, capacity: 4, loadTime: 1, unloadTime: 1),
            };

            var warehouses = new[] { factory, port, warehouseA, warehouseB };

            return new World(eventLogger, warehouses, transports, map);
        }
    }
}