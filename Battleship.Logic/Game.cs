using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic
{
    public class Game
    {
        private readonly int _size;
        private readonly IBoardSetter _boardSetter;
        private readonly Board _playerBoard;
        private readonly Board _computerBoard;

        public Game(int size, IBoardSetter boardSetter)
        {
            _size = size;
            _boardSetter = boardSetter;
            _playerBoard = new Board(size);
            _computerBoard = new Board(size);
        }

        public void PrepareGame()
        {
            _boardSetter.SetupBoard(_playerBoard);
            _boardSetter.SetupBoard(_computerBoard);
        }
    }
}
