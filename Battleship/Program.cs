using Battleship.Logic;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(10, new BoardSetter());
            game.PrepareGame();
            var gameUI = new GameUI(game);
            gameUI.PrintUI();
        }
    }
}
