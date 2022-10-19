using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic.BoardSetting
{
    public class DefaultBoardSetter : IBoardSetter
    {
        public void SetupBoard(ISettable board)
        {
            var takenCoordinates = new List<Coordinate>();
            SetupBattleship(board, takenCoordinates, Consts.BATTLESHIP_ID);
            SetupDestroyer(board, takenCoordinates, Consts.DESTROYER_1_ID);
            SetupDestroyer(board, takenCoordinates, Consts.DESTROYER_2_ID);
        }

        private void SetupShip(ISettable board, 
            List<Coordinate> takenCoordinates, 
            int shipId,
            int shipLength,
            EFieldType shipType)
        {
            var direction = GetRandomDirection();
            int startX, startY;
            var maxPosition = board.Size;
            var minPosition = 0;

            if (direction == EDirection.Vertical)
                SetupVerticalShip();
            else
                SetupHorizontalShip();

            void SetupVerticalShip()
            {
                do
                {
                    startX = GetRandomCoordinate(minPosition, maxPosition - shipLength);
                    startY = GetRandomCoordinate(minPosition, maxPosition);
                } while (SelectedCoordinatesAreOnForbiddenList(takenCoordinates, startX, startY, EDirection.Vertical));
                for (int i = startX; i < startX + shipLength; i++)
                {
                    board.SetFieldType(i, startY, shipType, shipId);
                    takenCoordinates.Add(new Coordinate(i, startY));
                }
            }

            void SetupHorizontalShip()
            {
                do
                {
                    startX = GetRandomCoordinate(minPosition, maxPosition);
                    startY = GetRandomCoordinate(minPosition, maxPosition - shipLength);
                } while (SelectedCoordinatesAreOnForbiddenList(takenCoordinates, startX, startY, EDirection.Horizontal));

                for (int i = startY; i < startY + shipLength; i++)
                {
                    board.SetFieldType(startX, i, shipType, shipId);
                    takenCoordinates.Add(new Coordinate(startX, i));
                }
            }
        }

        private void SetupBattleship(ISettable board, List<Coordinate> takenCoordinates, int shipId)
        {
            SetupShip(board, takenCoordinates, shipId, Consts.BATTLESHIP_LENGTH, EFieldType.Battleship);
        }

        private void SetupDestroyer(ISettable board, List<Coordinate> takenCoordinates, int shipId)
        {
            SetupShip(board, takenCoordinates, shipId, Consts.DESTROYER_LENGTH, EFieldType.Destroyer);
        }

        private bool SelectedCoordinatesAreOnForbiddenList(IEnumerable<Coordinate> forbiddenCoordinates, int x, int y, EDirection direction)
        {
            if (direction == EDirection.Vertical)
            {
                for (int i = x; i < x + Consts.DESTROYER_LENGTH; i++)
                {
                    if (forbiddenCoordinates.Any(coordinate => coordinate.X == i && coordinate.Y == y))
                        return true;
                }
            }
            else
            {
                for (int i = y; i < y + Consts.DESTROYER_LENGTH; i++)
                {
                    if (forbiddenCoordinates.Any(coordinate => coordinate.X == x && coordinate.Y == i))
                        return true;
                }
            }
            return false;
        }

        private int GetRandomCoordinate(int min, int max)
            => new Random().Next(min, max);
        private EDirection GetRandomDirection()
            => new Random().Next(2) > 0 ? EDirection.Horizontal : EDirection.Vertical;
    }

    class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    enum EDirection
    {
        Horizontal,
        Vertical
    }
}
