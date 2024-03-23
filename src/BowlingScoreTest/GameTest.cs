using BowlingScoreInterface.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BowlingScoreInterface.Tests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Constructor_WithHomeParameter_SetsPropertiesCorrectly()
        {
            var home = new Home { Players = new List<string> { "Alice", "Bob" }, NumberOfRounds = 10 };

            var game = new Game(home);
            Assert.AreEqual(2, game.Players.Count);
            Assert.IsTrue(game.Players[0].Name=="Alice");
            Assert.IsTrue(game.Players[1].Name=="Bob");
            Assert.AreEqual(11, game.NumberOfRounds);
        }
    }
}
