using Battleship.Logic.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleship.Tests.Unit.Validation
{
    public class CoordinatesValidatorTests
    {
        private const int GAME_SIZE = 10;

        [Theory]
        [InlineData(1, 1)]
        [InlineData(9, 1)]
        [InlineData(4, 2)]
        [InlineData(1, 7)]
        [InlineData(0, 2)]
        [InlineData(0, 0)]
        public void Should_return_true_if_coordinates_are_lower_then_game_size_and_greater_than_0(int row, int column)
        {
            var result = new CoordinatesValidator(GAME_SIZE).IsValid(row, column);
            Assert.True(result);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(9, -1)]
        [InlineData(4, -2)]
        [InlineData(11, 7)]
        [InlineData(0, 12)]
        [InlineData(0, 10)]
        public void Should_return_false_if_coordinates_are_graterEqual_then_game_size_or_lower_than_0(int row, int column)
        {
            var result = new CoordinatesValidator(GAME_SIZE).IsValid(row, column);
            Assert.False(result);
        }
    }
}
