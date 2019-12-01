using NUnit.Framework;
using TransportTycoon.Models;
using TransportTycoon.Loggers;

namespace TransportTycoon.Tests
{
    public class WorldTest
    {
        [Test]
        public void Solve_A()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("A", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_B()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_AA()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("A,A", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(13));
        }

        [Test]
        public void Solve_BB()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("B,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_AB()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("A,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_ABB()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("A,B,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(7));
        }

        [Test]
        public void Solve_AABABBAB()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("A,A,B,A,B,B,A,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(29));
        }

        [Test]
        public void Solve_ABBBABAAABBB()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise1("A,B,B,B,A,B,A,A,A,B,B,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(41));
        }

        [Test]
        public void Solve_A_LoadUnloadShip()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise2("A", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(9));
        }

        [Test]
        public void Solve_B_LoadUnloadShip()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise2("B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_AA_LoadUnloadShip()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise2("A,A", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(9));
        }

        [Test]
        public void Solve_BB_LoadUnloadShip()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise2("B,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_AB_LoadUnloadShip()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise2("A,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(9));
        }

        [Test]
        public void Solve_ABB_LoadUnloadShip()
        {
            var logger = new EmptyEventLogger();
            var world = WorldBuilder.CreateForExercise2("A,B,B", logger);
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(9));
        }

        [Test]
        public void Solve_AB_Logs()
        {
            var logger = new MemoryEventLogger();
            var world = WorldBuilder.CreateForExercise2("A,B", logger);
            var hours = world.Solve();

            var actual = logger.Logs;
            var expected = new [] 
            {
                "{\"event\":\"DEPART\",\"time\":0,\"transport_id\":0,\"kind\":\"Truck\",\"location\":\"FACTORY\",\"destination\":\"PORT\",\"cargo\":[{\"cargo_id\":0,\"destination\":\"A\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"DEPART\",\"time\":0,\"transport_id\":1,\"kind\":\"Truck\",\"location\":\"FACTORY\",\"destination\":\"B\",\"cargo\":[{\"cargo_id\":1,\"destination\":\"B\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"ARRIVE\",\"time\":1,\"transport_id\":0,\"kind\":\"Truck\",\"location\":\"PORT\",\"cargo\":[{\"cargo_id\":0,\"destination\":\"A\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"DEPART\",\"time\":1,\"transport_id\":0,\"kind\":\"Truck\",\"location\":\"PORT\",\"destination\":\"FACTORY\",\"cargo\":[]}",
                "{\"event\":\"LOAD\",\"time\":1,\"duration\":1,\"transport_id\":2,\"kind\":\"Ship\",\"location\":\"PORT\",\"cargo\":[{\"cargo_id\":0,\"destination\":\"A\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"ARRIVE\",\"time\":2,\"transport_id\":0,\"kind\":\"Truck\",\"location\":\"FACTORY\",\"cargo\":[]}",
                "{\"event\":\"DEPART\",\"time\":2,\"transport_id\":2,\"kind\":\"Ship\",\"location\":\"PORT\",\"destination\":\"A\",\"cargo\":[{\"cargo_id\":0,\"destination\":\"A\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"ARRIVE\",\"time\":5,\"transport_id\":1,\"kind\":\"Truck\",\"location\":\"B\",\"cargo\":[{\"cargo_id\":1,\"destination\":\"B\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"DEPART\",\"time\":5,\"transport_id\":1,\"kind\":\"Truck\",\"location\":\"B\",\"destination\":\"FACTORY\",\"cargo\":[]}",
                "{\"event\":\"ARRIVE\",\"time\":8,\"transport_id\":2,\"kind\":\"Ship\",\"location\":\"A\",\"cargo\":[{\"cargo_id\":0,\"destination\":\"A\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"UNLOAD\",\"time\":8,\"duration\":1,\"transport_id\":2,\"kind\":\"Ship\",\"location\":\"PORT\",\"cargo\":[{\"cargo_id\":0,\"destination\":\"A\",\"origin\":\"FACTORY\"}]}",
                "{\"event\":\"DEPART\",\"time\":9,\"transport_id\":2,\"kind\":\"Ship\",\"location\":\"A\",\"destination\":\"PORT\",\"cargo\":[]}"
            };
            Assert.That(actual.Count, Is.EqualTo(12));
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}