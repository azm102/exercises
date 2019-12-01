
using System;
using System.Collections.Generic;
using System.Linq;
using TransportTycoon.Events;

namespace TransportTycoon.Models
{
    public class Transport
    {
        private readonly List<TransportTask> _tasks = new List<TransportTask>();

        public Transport(
            long id,
            string name,
            Warehouse location,
            TransportType type,
            int capacity,
            int loadTime,
            int unloadTime)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            Capacity = capacity;
            LoadTime = loadTime;
            UnloadTime = unloadTime;
        }

        public long Id { get; }
        public string Name { get; }
        public TransportType Type { get; }
        public int Capacity { get; }
        public int LoadTime { get; }
        public int UnloadTime { get; }

        public Warehouse Location { get; private set; }
        public Warehouse Destination { get; private set; }
        public int TransferTime { get; private set; }
        public IReadOnlyList<Cargo> Cargos { get; private set; } = new List<Cargo>();
        public bool IsCompleted => !_tasks.Any();

        public event Action<LoadEvent> OnLoadEvent;
        public event Action<UnloadEvent> OnUnloadEvent;
        public event Action<DepartEvent> OnDepartEvent;
        public event Action<ArriveEvent> OnArriveEvent;

        public void Transfer(Warehouse location, Warehouse destination, int transferTime, IReadOnlyList<Cargo> cargos)
        {
            Location = location;
            Destination = destination;
            TransferTime = transferTime;
            Cargos = cargos;

            if (cargos.Any())
            {
                var loadTask = new TransportTask("Load", LoadTime);
                if (0 < LoadTime)
                {
                    loadTask.OnStartedEvent += () => OnLoadEvent?.Invoke(new LoadEvent(this, location, LoadTime, cargos));
                }
                AddTask(loadTask);
            }

            var transferTask = new TransportTask("Transfer", transferTime);
            transferTask.OnStartedEvent += () => OnDepartEvent?.Invoke(new DepartEvent(this, location, destination, cargos));
            transferTask.OnCompletedEvent += () =>
            {
                Location = destination;
                Destination = null;
                OnArriveEvent?.Invoke(new ArriveEvent(this, destination, cargos));
            };
            AddTask(transferTask);

            if (cargos.Any())
            {
                var unloadTask = new TransportTask("Unload", UnloadTime);
                unloadTask.OnStartedEvent += () =>
                {
                    if (0 < UnloadTime)
                    {
                        OnUnloadEvent?.Invoke(new UnloadEvent(this, location, UnloadTime, cargos));
                    }
                };
                unloadTask.OnCompletedEvent += () =>
                {
                    destination.Cargos.AddRange(Cargos);
                    Cargos = new List<Cargo>();
                    Transfer(destination, location, transferTime, new List<Cargo>());
                };
                AddTask(unloadTask);
            }
        }

        private void AddTask(TransportTask task)
        {
            if (!_tasks.Any())
            {
                task.DoOnStarted();
                if (task.ExecutionTime == 0)
                {
                    task.DoOnCompleted();
                }
                else
                {
                    _tasks.Add(task);
                }
            }
            else
            {
                _tasks.Add(task);
            }
        }

        public void Inc()
        {
            if (_tasks.Any())
            {
                var firstTask = _tasks.First();
                firstTask.Inc();
                if (firstTask.IsCompleted)
                {
                    firstTask.DoOnCompleted();
                    _tasks.Remove(firstTask);

                    while (true)
                    {
                        if (_tasks.Any())
                        {
                            var nextTask = _tasks.First();
                            nextTask.DoOnStarted();
                            if (nextTask.ExecutionTime == 0)
                            {
                                nextTask.DoOnCompleted();
                                _tasks.Remove(nextTask);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        public override string ToString() => $"{Id} {Type} {Name}";
    }
}