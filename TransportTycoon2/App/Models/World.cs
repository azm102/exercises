using System.Collections.Generic;
using System.Linq;
using TransportTycoon.Events;
using TransportTycoon.Utils;
using TransportTycoon.Loggers;
using System;

namespace TransportTycoon.Models
{
    public class World
    {
        private const int MaxCyclesCount = 1000;
        private readonly IReadOnlyList<Warehouse> _warehouses;
        private readonly IReadOnlyList<Transport> _transports;
        private readonly Map _map;
        private readonly IEventLogger _eventLogger;
        private int _time = 0;

        public World(
            IEventLogger eventLogger,
            IReadOnlyList<Warehouse> warehouses,
            IReadOnlyList<Transport> transports,
            Map map)
        {
            _eventLogger = eventLogger;
            _warehouses = warehouses;
            _transports = transports;
            _map = map;

            _transports.ForEach(x =>
            {
                x.OnLoadEvent += x => { x.Time = _time; _eventLogger.Log(x); };
                x.OnUnloadEvent += x => { x.Time = _time; _eventLogger.Log(x); };
                x.OnDepartEvent += x => { x.Time = _time; _eventLogger.Log(x); };
                x.OnArriveEvent += x => { x.Time = _time; _eventLogger.Log(x); };
            });
        }

        public int Solve()
        {
            _time = 0;

            while (true)
            {
                var freeTransports = GetFreeTransports();
                freeTransports.ForEach(TakeCargoFromWarehouse);

                _time += 1;

                UpdateTransports();

                if (IsCompleted())
                {
                    break;
                }

                if (MaxCyclesCount < _time)
                {
                    throw new Exception("Oops, you are stuck!");
                }
            }

            return _time;
        }

        private IReadOnlyList<Transport> GetFreeTransports()
        {
            var transports = _transports
                .Where(x => x.IsCompleted)
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

        private (IReadOnlyList<Cargo>, Road) GetOptimalCargoForTransport(Transport transport)
        {
            // v1: take first element in warehouse queue
            var warehouse = transport.Location;
            var optimal = warehouse.Cargos
                .GroupBy(x => x.Destination)
                .Select(x =>
                {
                    var roads = _map.GetPathFromTo(warehouse, x.Key);
                    var acceptable = roads.Any() && roads.First().Type == transport.Type;
                    return new
                    {
                        Acceptable = acceptable,
                        Cargos = x.Take(transport.Capacity).ToList(),
                        Roads = roads
                    };
                })
                .FirstOrDefault(x => x.Acceptable);

            if (optimal != null)
            {
                return (optimal.Cargos, optimal.Roads.First());
            }
            return (null, null);
        }

        private void PutCargoToTransport(Transport transport, IReadOnlyList<Cargo> cargos, Road road)
        {
            var warehouse = transport.Location;
            cargos.ForEach(x => warehouse.Cargos.Remove(x));
            transport.Transfer(road.From, road.To, road.Time, cargos);
        }

        private void UpdateTransports()
        {
            _transports
                .Where(x => !x.IsCompleted)
                .ForEach(x =>
                {
                    x.Inc();
                });
        }

        private bool IsCompleted()
        {
            var allCargosDelivered = _warehouses.All(x => x.Cargos.All(y => y.Destination == x));
            var transportsIsEmpty = _transports.All(x => !x.Cargos.Any());
            return allCargosDelivered && transportsIsEmpty;
        }
    }
}