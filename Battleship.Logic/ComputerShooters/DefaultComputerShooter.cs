using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic.ComputerShooters
{
    public class DefaultComputerShooter : IComputerShooter
    {
        private readonly List<Coordinate> _shotCoordinates;
        private int _gameSize;
        public DefaultComputerShooter(int gameSize)
        {
            _shotCoordinates = new List<Coordinate>();
            _gameSize = gameSize;
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
            => new Random().Next(min, max);
    }
}

