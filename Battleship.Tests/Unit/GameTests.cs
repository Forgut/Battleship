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
        //TODO game methods tests
        [Fact]
        public void PrepareGame_should_mark_player_turn_and_setup_board()
        {
            var boardSetter = Substitute.For<IBoardSetter>();
            var game = new Game(GAME_SIZE, boardSetter, Substitute.For<IBoardShooter>());
            game.PrepareGame();
            Assert.Equal(ETurn.Player, game.Turn);
            boardSetter.Received(2).SetupBoard(Arg.Any<ISettable>());
        }
    }
}
