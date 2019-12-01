using System;
using TransportTycoon.Models;
using TransportTycoon.Loggers;
using TransportTycoon.Utils;

namespace TransportTycoon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SolveInput(args);
            //CreateTraces();
        }

        private static void SolveInput(string[] args)
        {
            var input = args[0];
            var eventLogger = new FileEventLogger(input.Replace(",", ""));
            var world = WorldBuilder.CreateForExercise2(input, eventLogger);
            var totalTime = world.Solve();
            Console.WriteLine($"Total time {totalTime}");
        }

        private static void CreateTraces()
        {
            var inputs = new[]
            {
                "A",
                "B",
                "A,A",
                "B,B",
                "A,B",
                "A,B,B",
                "A,A,B,A,B,B,A,B",
                "A,B,B,B,A,B,A,A,A,B,B,B"
            };

            inputs.ForEach(x =>
            {
                var eventLogger = new FileEventLogger(x.Replace(",", ""));
                var world = WorldBuilder.CreateForExercise2(x, eventLogger);
                var totalTime = world.Solve();
                Console.WriteLine($"Solve '{x}' total time '{totalTime}'");
            });
        }
    }
}
