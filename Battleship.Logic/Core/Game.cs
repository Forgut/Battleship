using Battleship.Logic.BoardSetting;
using Battleship.Logic.Core.Enums;
using Battleship.Logic.ShootingLogic;
using System.Linq;

namespace Battleship.Logic.Core
{
    public class Game
    {
        private readonly IBoardSetter _boardSetter;
        private readonly IBoardShooter _boardShooter;

        public Game(int size, IBoardSetter boardSetter, IBoardShooter boardShooter)
        {
            Size = size;
            _boardSetter = boardSetter;
            PlayerBoard = new Board(size);
            ComputerBoard = new Board(size);
            _boardShooter = boardShooter;
        }

        public int Size { get; private set; }
        public ETurn Turn { get; private set; }
        public Board PlayerBoard { get; private set; }
        public Board ComputerBoard { get; private set; }

        public void PrepareGame()
        {
            _boardSetter.SetupBoard(PlayerBoard);
            _boardSetter.SetupBoard(ComputerBoard);
            Turn = ETurn.Player;
        }

        public EHitResult ShootAtPosition(int row, int column)
        {
            if (Turn == ETurn.Player)
                return _boardShooter.ShootAtPosition(ComputerBoard, row, column);
            else
                return _boardShooter.ShootAtPosition(PlayerBoard, row, column);
        }

        public EGameResult CheckGameState()
        {
            if (!PlayerBoard.Fields
                .Any(row => row
                .Any(field => field.Type != EFieldType.Water && !field.IsHit)))
                return EGameResult.ComputerWins;

            if (!ComputerBoard.Fields
                .Any(row => row
                .Any(field => field.Type != EFieldType.Water && !field.IsHit)))
                return EGameResult.PlayerWins;

            return EGameResult.GoesOn;
        }

        public void ToggleTurnMarker()
        {
            if (Turn == ETurn.Computer)
                Turn = ETurn.Player;
            else
                Turn = ETurn.Computer;
        }
    }
}
