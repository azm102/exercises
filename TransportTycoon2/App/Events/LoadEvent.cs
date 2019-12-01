using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TransportTycoon.Models;

namespace TransportTycoon.Events
{
    public class LoadEvent : IEvent
    {
        private readonly Transport _transport;
        private readonly Warehouse _location;
        private readonly int _duration;
        private readonly IReadOnlyList<Cargo> _cargos;

        public LoadEvent(
            Transport transport, 
            Warehouse location, 
            int duration,
            IReadOnlyList<Cargo> cargos)
        {
            _transport = transport;
            _location = location;
            _duration = duration;
            _cargos = cargos;
        }

        public int Time { get; set; }

        public override string ToString()
        {
            var log = new
            {
                @event = "LOAD",
                time = Time,
                duration = _duration,
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