using Battleship.CoordinatesParser;
using Battleship.Logic.BoardSetting;
using Battleship.Logic.ComputerShooters;
using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using Battleship.Logic.ShootingLogic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameUI = PrepareGameComponents();
            GameLoop(gameUI);
        }

        private static GameUI PrepareGameComponents()
        {
            const int GAME_SIZE = 10;
            IBoardSetter boardSetter = new DefaultBoardSetter();
            IBoardShooter boardShooter = new DefaultBoardShooter();
            var game = new Game(GAME_SIZE, boardSetter, boardShooter);
            game.PrepareGame();

            var coordinatesParser = new DefaultCoordinatesParser(GAME_SIZE);
            var computerShooter = new DefaultComputerShooter(GAME_SIZE);
            return new GameUI(game, coordinatesParser, computerShooter);
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
