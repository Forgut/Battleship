using Battleship.ComputerShooters;
using Battleship.CoordinatesParser;
using Battleship.Logic.BoardSetting;
using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using Battleship.Logic.ShootingLogic;
using System;
using System.Linq;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var debugMode = ShouldRunInDebugMode(args);
            var gameUI = PrepareGameComponents(debugMode);
            GameLoop(gameUI);
        }

        private static bool ShouldRunInDebugMode(string[] args)
        {
            if (args?.Any() != true)
                return false;

            if (bool.TryParse(args.First(), out var result))
                return result;

            return false;
        }

        private static GameUI PrepareGameComponents(bool runInDebugMode)
        {
            const int GAME_SIZE = 10;
            IBoardSetter boardSetter = new DefaultBoardSetter();
            IBoardShooter boardShooter = new DefaultBoardShooter();
            var game = new Game(GAME_SIZE, boardSetter, boardShooter);
            game.PrepareGame();

            if (runInDebugMode)
            {
                Console.WriteLine("Game is running in DEBUG mode - you will be able to see enemy position.\nPress any key to continue");
                Console.ReadKey();
            }

            var coordinatesParser = new DefaultCoordinatesParser(GAME_SIZE);
            var computerShooter = new DefaultComputerShooter(GAME_SIZE);
            return new GameUI(game, coordinatesParser, computerShooter)
            {
                DebugMode = runInDebugMode,
            };
        }

        private static void GameLoop(GameUI gameUI)
        {
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
