using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using TransportTycoon.Models;

namespace TransportTycoon.Events
{
    public class ArriveEvent : IEvent
    {
        private readonly Transport _transport;
        private readonly Warehouse _location;
        private readonly IReadOnlyList<Cargo> _cargos;

        public ArriveEvent(
            Transport transport,
            Warehouse location,
            IReadOnlyList<Cargo> cargos)
        {
            _transport = transport;
            _location = location;
            _cargos = cargos;
        }

        public int Time { get; set; }

        public override string ToString()
        {
            var log = new
            {
                @event = "ARRIVE",
                time = Time,
                transport_id = _transport.Id,
                kind = _transport.Type.ToString(),
                location = _location.Name,
                cargo = _cargos.Select(x => new { cargo_id = x.Id, destination = x.Destination.Name, origin = x.Origin.Name })
            };
            var json = JsonSerializer.Serialize(log);
            return json;
        }
    }
}