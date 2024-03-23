using BowlingScoreInterface.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BowlingScoreInterface.Tests
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Act
            var home = new Home();

            // Assert
            Assert.IsNotNull(home.Players);
            Assert.AreEqual(0, home.Players.Count);
            Assert.AreEqual(10, home.NumberOfRounds);
            Assert.AreEqual(10, home.NumberOfPins);
            Assert.AreEqual(10, home.MaxPlayers);
        }

        [TestMethod]
        public void Constructor_InitializesWithGivenValues()
        {
            // Arrange
            var players = new List<string> { "Alice", "Bob" };
            var numberOfRounds = 5;
            var numberOfPins = 8;
            var maxPlayers = 10;

            // Act
            var home = new Home
            {
                Players = players,
                NumberOfRounds = numberOfRounds,
                NumberOfPins = numberOfPins
            };

            // Assert
            Assert.AreEqual(players, home.Players);
            Assert.AreEqual(numberOfRounds, home.NumberOfRounds);
            Assert.AreEqual(numberOfPins, home.NumberOfPins);
            Assert.AreEqual(maxPlayers, home.MaxPlayers);
        }

    }
}
