using System;

namespace TransportTycoon.Models
{
    public class TransportTask
    {
        private int _time;

        public TransportTask(
            string name, 
            int executionTime)
        {
            Name = name;
            ExecutionTime = executionTime;
        }

        public string Name { get; }
        public int ExecutionTime { get; }
        public bool IsCompleted => ExecutionTime <= _time;
        public event Action OnStartedEvent;
        public event Action OnCompletedEvent;

        public void Inc()
        {
            if (ExecutionTime <= _time)
            {
                return;
            }
            _time++;
        }

        public void DoOnStarted() => OnStartedEvent?.Invoke();

        public void DoOnCompleted() => OnCompletedEvent?.Invoke();
    }
}