using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic
{
    public class Game
    {
        private readonly IBoardSetter _boardSetter;

        public Game(int size, IBoardSetter boardSetter)
        {
            Size = size;
            _boardSetter = boardSetter;
            PlayerBoard = new Board(size);
            ComputerBoard = new Board(size);
        }

        public int Size { get; private set; }
        public ETurn Turn { get; private set; }
        public Board PlayerBoard { get; private set; }
        public Board ComputerBoard { get; private set; }

        public void PrepareGame()
        {
            _boardSetter.SetupBoard(PlayerBoard);
            _boardSetter.SetupBoard(ComputerBoard);
        }


    }

    public enum ETurn
    {
        Player,
        Computer,
    }
}
