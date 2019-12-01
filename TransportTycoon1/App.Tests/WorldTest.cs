using NUnit.Framework;
using App;

namespace App.Tests
{
    public class WorldTest
    {
        [Test]
        public void Solve_A()
        {
            var world = new World("A");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_B()
        {
            var world = new World("B");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_AA()
        {
            var world = new World("A,A");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(13));
        }

        [Test]
        public void Solve_BB()
        {
            var world = new World("B,B");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_AB()
        {
            var world = new World("A,B");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(5));
        }

        [Test]
        public void Solve_ABB()
        {
            var world = new World("A,B,B");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(7));
        }

        [Test]
        public void Solve_AABABBAB()
        {
            var world = new World("A,A,B,A,B,B,A,B");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(29));
        }

        [Test]
        public void Solve_ABBBABAAABBB()
        {
            var world = new World("A,B,B,B,A,B,A,A,A,B,B,B");
            var hours = world.Solve();
            Assert.That(hours, Is.EqualTo(41));
        }
    }
}