using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class DefaultCoordinatesParserTests
    {
        private const int GAME_SIZE = 10;

        [Theory]
        [InlineData("asdas")]
        [InlineData("")]
        [InlineData("AAAA5")]
        [InlineData(null)]
        public void Should_return_unsuccessful_result_with_wrong_text_parameters(string input)
        {
            var parser = new DefaultCoordinatesParser(GAME_SIZE);
            var result = parser.Parse(input);
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData("A11")]
        [InlineData("K1")]
        [InlineData("K11")]
        public void Should_return_unsuccessful_result_for_text_parameters_outside_of_game_size(string input)
        {
            var parser = new DefaultCoordinatesParser(GAME_SIZE);
            var result = parser.Parse(input);
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData("A1", 0, 0)]
        [InlineData("B3", 1, 2)]
        [InlineData("J10", 9, 9)]
        [InlineData("I4", 8, 3)]
        public void Should_return_successful_result_for_proper_text_parameters(string input, int expectedX, int expectedY)
        {
            var parser = new DefaultCoordinatesParser(GAME_SIZE);
            var result = parser.Parse(input);
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedX, result.Row);
            Assert.Equal(expectedY, result.Column);
        }


        [Theory]
        [InlineData(-1, 2)]
        [InlineData(2, -1)]
        [InlineData(11, 1)]
        [InlineData(1, 11)]
        public void Should_return_unsuccessful_result_for_coordinate_parameters_outside_of_game_size(int row, int column)
        {
            var parser = new DefaultCoordinatesParser(GAME_SIZE);
            var result = parser.Parse(row, column);
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData(0, 0, "A1")]
        [InlineData(1, 2, "B3")]
        [InlineData(9, 9, "J10")]
        [InlineData(8, 3, "I4")]
        public void Should_return_successful_result_for_proper_coordinate_parameters(int row, int column, string expectedText)
        {
            var parser = new DefaultCoordinatesParser(GAME_SIZE);
            var result = parser.Parse(row, column);
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedText, result.Text);
        }
    }
}
