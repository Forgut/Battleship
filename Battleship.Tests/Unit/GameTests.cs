using Battleship.Logic.BoardSetting;
using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using Battleship.Logic.ShootingLogic;
using NSubstitute;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class GameTests
    {
        private const int GAME_SIZE = 10;

        [Fact]
        public void PrepareGame_should_mark_player_turn_and_setup_board()
        {
            var boardSetter = Substitute.For<IBoardSetter>();
            var game = new Game(GAME_SIZE, boardSetter, Substitute.For<IBoardShooter>());
            game.PrepareGame();
            Assert.Equal(ETurn.Player, game.Turn);
            Assert.Equal(GAME_SIZE, game.Size);
            boardSetter.Received(2).SetupBoard(Arg.Any<ISettable>());
        }

        [Fact]
        public void ToggleTurnMarker_should_switch_turn_to_opposite_player()
        {
            var game = new Game(GAME_SIZE, Substitute.For<IBoardSetter>(), Substitute.For<IBoardShooter>());
            Assert.Equal(ETurn.Player, game.Turn);
            game.ToggleTurnMarker();
            Assert.Equal(ETurn.Computer, game.Turn);
            game.ToggleTurnMarker();
            Assert.Equal(ETurn.Player, game.Turn);
        }

        [Fact]
        public void CheckGameState_should_return_GameGoesOn_when_both_players_have_not_sunken_shit()
        {
            var game = new Game(GAME_SIZE, Substitute.For<IBoardSetter>(), Substitute.For<IBoardShooter>());
            game.PlayerBoard.SetFieldType(0, 0, EFieldType.Destroyer, 0);
            game.ComputerBoard.SetFieldType(0, 0, EFieldType.Destroyer, 0);
            var gameState = game.CheckGameState();
            Assert.Equal(EGameResult.GoesOn, gameState);
        }

        [Fact]
        public void CheckGameState_should_return_PlayerWin_when_computer_has_no_more_ships_to_sink()
        {
            var game = new Game(GAME_SIZE, Substitute.For<IBoardSetter>(), Substitute.For<IBoardShooter>());
            game.PlayerBoard.SetFieldType(0, 0, EFieldType.Destroyer, 0);
            var gameState = game.CheckGameState();
            Assert.Equal(EGameResult.PlayerWins, gameState);
        }

        [Fact]
        public void CheckGameState_should_return_ComputerWin_when_player_has_no_more_ships_to_sink()
        {
            var game = new Game(GAME_SIZE, Substitute.For<IBoardSetter>(), Substitute.For<IBoardShooter>());
            game.ComputerBoard.SetFieldType(0, 0, EFieldType.Destroyer, 0);
            var gameState = game.CheckGameState();
            Assert.Equal(EGameResult.ComputerWins, gameState);
        }

        [Fact]
        public void ShootAtPosition_should_invoke_shooting_logic_on_proper_board_depending_on_turn()
        {
            var shooter = Substitute.For<IBoardShooter>();
            var game = new Game(GAME_SIZE, Substitute.For<IBoardSetter>(), shooter);
            game.ShootAtPosition(0, 0);
            shooter.Received(1).ShootAtPosition(game.ComputerBoard, 0, 0);
            game.ToggleTurnMarker();
            game.ShootAtPosition(0, 0);
            shooter.Received(1).ShootAtPosition(game.PlayerBoard, 0, 0);
        }
    }
}
