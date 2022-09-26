using Battleship.Logic;
using System;
using Xunit;
using System.Linq;

namespace Battleship.Tests
{
    public class BoardSetterTests
    {
        [Fact]
        public void Should_setup_battleship_as_5_fields_on_board()
        {
            var boardSetter = new BoardSetter();
            var board = new Board(10);
            boardSetter.SetupBoard(board);
            AssertCorrectCountOnBoard(board, EFieldType.Battleship, Consts.BATTLESHIP_LENGTH);
            AssertCorrectPlacementOnBoard(board, EFieldType.Battleship, Consts.BATTLESHIP_ID, Consts.BATTLESHIP_LENGTH);
        }

        [Fact]
        public void Should_setup_destroyers_as_2_times_4_fields_on_board()
        {
            var boardSetter = new BoardSetter();
            var board = new Board(10);
            boardSetter.SetupBoard(board);
            AssertCorrectCountOnBoard(board, EFieldType.Destroyer, Consts.DESTROYER_LENGTH * 2);
            AssertCorrectPlacementOnBoard(board, EFieldType.Destroyer, Consts.DESTROYER_1_ID, Consts.DESTROYER_LENGTH);
            AssertCorrectPlacementOnBoard(board, EFieldType.Destroyer, Consts.DESTROYER_2_ID, Consts.DESTROYER_LENGTH);
        }

        private void AssertCorrectCountOnBoard(Board board, EFieldType type, int expectedCount)
        {
            Assert.True(board.Fields.Sum(x => x.Count(y => y.Type == type)) == expectedCount);
        }

        private void AssertCorrectPlacementOnBoard(Board board, EFieldType type, int shipId, int expectedCount)
        {
            //TODO check if fields are in one group
            var onlyOneColumnContainsExpectedShipId =
                board.Fields.Count(row => row.Count(field => field.Type == type && field.ShipId == shipId) == expectedCount) == 1;
            var onlyOneRowContainsExpectedShipId =
                board.Fields.Count(row => row.Count(field => field.Type == type && field.ShipId == shipId) == 1) == expectedCount;

            Assert.True(onlyOneColumnContainsExpectedShipId | onlyOneRowContainsExpectedShipId);
        }
    }
}
