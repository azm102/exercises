using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = args[0];
            var world = new World(input);
            var totalTime = world.Solve();
            Console.WriteLine($"Total time {totalTime}");
        }
    }
}
