using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Battleships;
using FluentAssertions;
using Xunit;

namespace Battleships.Test
{
    public class GameTest
    {
        [Fact]
        public void TestPlay()
        {
            var ships = new[] { "3:2,3:5" };
            var guesses = new[] { "7:1", "3:3" };
            Game.Play(ships, guesses).Should().Be(1);
        }


        // this is fuzzy test for fun, not an actual unit test the way XUnit understands it
        [Fact]
        public void TestRandomGuess()
        {
            var ships = new[] { "3:2,3:5" };
            var random = new Random(); //
            var random_y = random.Next(2, 5 + 1);
            var guesses = new[] { $"3:{random_y}" };
            Game.Play(ships, guesses).Should().Be(1);
        }

        [Fact]
        public void Test0Guess()
        {
            // assert that app gracefully accepts 0 guesses
            var ships = new[] { "3:2,3:5" };
            var guesses = new string[0];
            Game.Play(ships, guesses).Should().Be(0);
        }


        [Fact]
        public void TestNoDiagonalShip()
        {
            // assert that app fails/throws if a ship is diagonal
            var ship = "1:1,10:10";
            Assert.Throws<InvalidDataException>(() => Ship.FromString(ship));
        }


        [Fact]
        public void TestBounds()
        {
            // assert that app fails/throws if ships are out of the 10x10 grid
            var ship = "11:1,11:11";
            Assert.Throws<InvalidDataException>(() => Ship.FromString(ship));

            ship = "10:0,10:19";
            Assert.Throws<InvalidDataException>(() => Ship.FromString(ship));
        }

        [Fact]
        public void TestLongship()
        {
            // assert that app fails/throws if ships are too long (range is 2-4)
            var ship = "3:1,3:5";
            Assert.Throws<InvalidDataException>(() => Ship.FromString(ship));
        }

        [Fact]
        public void TestShortship()
        {
            // assert that app fails/throws if ships are too short (range is 2-4)
            var ship = "3:1,3:1";
            Assert.Throws<InvalidDataException>(() => Ship.FromString(ship));
        }
    }
}
