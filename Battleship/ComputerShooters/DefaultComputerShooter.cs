using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.ComputerShooters
{
    public class DefaultComputerShooter : IComputerShooter
    {
        private readonly List<Coordinate> _shotCoordinates;
        private readonly int _gameSize;
        private readonly Random _rand;

        public DefaultComputerShooter(int gameSize, Random rand)
        {
            _shotCoordinates = new List<Coordinate>();
            _gameSize = gameSize;
            _rand = rand;
        }

        public Coordinate GetShotCoordinates()
        {
            int row = 0;
            int column = 0;

            do
            {
                row = GetRandomCoordinate(0, _gameSize);
                column = GetRandomCoordinate(0, _gameSize);
            } while (RowAndColumnWasGeneratedBefore());

            var result = new Coordinate(row, column);
            _shotCoordinates.Add(result);
            return result;

            bool RowAndColumnWasGeneratedBefore()
                => _shotCoordinates.Any(c => c.Row == row && c.Column == column);
        }

        private int GetRandomCoordinate(int min, int max)
            => _rand.Next(min, max);
    }
}

