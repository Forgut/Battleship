using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class BoardTests
    {
        private const int BOARD_SIZE = 10;

        [Theory]
        [InlineData(0, 0)]
        [InlineData(3, 4)]
        [InlineData(2, 8)]
        [InlineData(9, 9)]
        public void MarkFieldAsHit_should_mark_field_on_Field_array_as_hit(int row, int column)
        {
            var board = new Board(BOARD_SIZE);
            board.MarkFieldAsHit(row, column);
            Assert.True(board.Fields[column][row].IsHit);
        }

        [Fact]
        public void MarkFieldAsHit_should_return_miss_if_coordinates_are_outside_the_board()
        {
            var board = new Board(BOARD_SIZE);
            var result = board.MarkFieldAsHit(-1, -1);
            Assert.Equal(EHitResult.Miss, result);
        }

        [Fact]
        public void SetFieldType_should_set_new_type_to_field_and_mark_it_as_not_hit()
        {
            var board = new Board(BOARD_SIZE);
            board.SetFieldType(0, 0, EFieldType.Destroyer, 0);
            Assert.False(board.Fields[0][0].IsHit);
            Assert.Equal(EFieldType.Destroyer, board.Fields[0][0].Type);
        }

        [Fact]
        public void SetFieldType_should_set_nothing_if_coordinates_are_outside_the_board()
        {
            var board = new Board(BOARD_SIZE);
            board.SetFieldType(-1, -1, EFieldType.Destroyer, 0);
        }
    }
}
