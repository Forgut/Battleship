using Battleship.ComputerShooters;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class DefaultComputerShooterTests
    {
        private const int GAME_SIZE = 10;

        [Fact]
        public void Should_Generate_unique_coordinates_that_fit_in_game_size()
        {
            var rand = new Random(10);
            var shooter = new DefaultComputerShooter(GAME_SIZE, rand);

            var shotResults = new List<Coordinate>()
            {
                shooter.GetShotCoordinates(),
                shooter.GetShotCoordinates(),
                shooter.GetShotCoordinates(),
                shooter.GetShotCoordinates(),
                shooter.GetShotCoordinates(),
            };

            var shotResultsAreUnique = shotResults.Distinct().Count() == shotResults.Count;
            var allCoordinatesFitGameSize = !shotResults.Any(x => x.Column > GAME_SIZE || x.Column < 0 || x.Row > GAME_SIZE || x.Row < 0);
            Assert.True(shotResultsAreUnique);
            Assert.True(allCoordinatesFitGameSize);
        }
    }
}
