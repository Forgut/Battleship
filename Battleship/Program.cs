using Battleship.Logic;
using Battleship.Logic.BoardSetting;
using Battleship.Logic.ComputerShooters;
using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using Battleship.Logic.ShootingLogic;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(10, new BoardSetter(), new OneFieldBoardShooter());
            game.PrepareGame();
            var coordinatesParser = new DefaultCoordinatesParser(10);
            var computerShooter = new DefaultComputerShooter(10);
            var gameUI = new GameUI(game, coordinatesParser, computerShooter);
            do
            {
                gameUI.PrintUI();
                gameUI.PrintAndExecuteOrder();
                gameUI.EndTurn();
            } while (gameUI.CheckGameState() == EGameResult.GoesOn);

            gameUI.PrintAfterGameInfo();
        }
    }
}
