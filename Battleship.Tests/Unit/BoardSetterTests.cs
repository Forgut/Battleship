using Battleship.Logic;
using Battleship.Logic.BoardSetting;
using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using System;
using System.Linq;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class BoardSetterTests
    {
        [Fact]
        public void Should_setup_battleship_as_5_fields_on_board()
        {
            var random = new Random(10);
            var boardSetter = new DefaultBoardSetter(random);
            var board = new Board(10);
            boardSetter.SetupBoard(board);
            AssertCorrectCountOnBoard(board, EFieldType.Battleship, Consts.BATTLESHIP_LENGTH);
            AssertCorrectPlacementOnBoard(board, EFieldType.Battleship, Consts.BATTLESHIP_ID, Consts.BATTLESHIP_LENGTH);
        }

        [Fact]
        public void Should_setup_destroyers_as_2_times_4_fields_on_board()
        {
            var random = new Random(10);
            var boardSetter = new DefaultBoardSetter(random);
            var board = new Board(10);
            boardSetter.SetupBoard(board);
            AssertCorrectCountOnBoard(board, EFieldType.Destroyer, Consts.DESTROYER_LENGTH * 2);
            AssertCorrectPlacementOnBoard(board, EFieldType.Destroyer, Consts.DESTROYER_1_ID, Consts.DESTROYER_LENGTH);
            AssertCorrectPlacementOnBoard(board, EFieldType.Destroyer, Consts.DESTROYER_2_ID, Consts.DESTROYER_LENGTH);
        }

        private void AssertCorrectCountOnBoard(Board board, EFieldType type, int expectedCount)
        {
            Assert.Equal(expectedCount, board.Fields.Sum(x => x.Count(y => y.Type == type)));
        }

        private void AssertCorrectPlacementOnBoard(Board board, EFieldType type, int shipId, int expectedCount)
        {
            var onlyOneColumnContainsExpectedShipId =
                board.Fields.Count(row => row.Count(field => field.Type == type && field.ShipId == shipId) == expectedCount) == 1;
            var onlyOneRowContainsExpectedShipId =
                board.Fields.Count(row => row.Count(field => field.Type == type && field.ShipId == shipId) == 1) == expectedCount;

            Assert.True(onlyOneColumnContainsExpectedShipId | onlyOneRowContainsExpectedShipId);
        }
    }
}
