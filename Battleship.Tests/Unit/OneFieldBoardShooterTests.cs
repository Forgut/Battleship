using Battleship.Logic;
using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using Battleship.Logic.ShootingLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class OneFieldBoardShooterTests 
    {
        private const int BOARD_SIZE = 10;

        [Fact]
        public void Should_mark_field_as_hit_and_return_miss_for_water_hit()
        {
            var board = new Board(BOARD_SIZE);
            var shooter = new OneFieldBoardShooter();
            var result = shooter.ShootAtPosition(board, 3, 3);
            Assert.Equal(EHitResult.Miss, result);
            Assert.True(board.Fields[3][3].IsHit);
        }

        [Fact]
        public void Should_mark_field_as_hit_and_return_hit_for_destroyer_that_is_not_last_destroyer_on_board()
        {
            var board = new Board(BOARD_SIZE);
            board.SetFieldType(3, 3, EFieldType.Destroyer, 1);
            board.SetFieldType(3, 4, EFieldType.Destroyer, 1);
            var shooter = new OneFieldBoardShooter();
            var result = shooter.ShootAtPosition(board, 3, 3);
            Assert.True(board.Fields[3][3].IsHit);
            Assert.Equal(EHitResult.Hit, result);
        }

        [Fact]
        public void Should_mark_field_as_hit_and_return_sunk_for_destroyer_that_is_last_destroyer_on_board()
        {
            var board = new Board(BOARD_SIZE);
            board.SetFieldType(3, 3, EFieldType.Destroyer, 1);
            var shooter = new OneFieldBoardShooter();
            var result = shooter.ShootAtPosition(board, 3, 3);
            Assert.True(board.Fields[3][3].IsHit);
            Assert.Equal(EHitResult.Sunk, result);
        }
    }
}
