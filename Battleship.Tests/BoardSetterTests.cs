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
            AssertCorrectCountOfFieldTypeOnBoard(board, EFieldType.Battleship, Consts.BATTLESHIP_LENGTH);
            //TODO check if they are in one row or column
        }

        [Fact]
        public void Should_setup_destroyers_as_2_times_4_fields_on_board()
        {
            var boardSetter = new BoardSetter();
            var board = new Board(10);
            boardSetter.SetupBoard(board);
            AssertCorrectCountOfFieldTypeOnBoard(board, EFieldType.Destroyer, Consts.DESTROYER_LENGTH * 2);
            //TODO check if they are in one row or column
        }

        private void AssertCorrectCountOfFieldTypeOnBoard(Board board, EFieldType type, int expectedCount)
        {
            Assert.True(board.Fields.Sum(x => x.Count(y => y.Type == type)) == expectedCount);
        }

        private void AssertCorrectPlacementOfFieldTypeOnBoard(Board board, EFieldType type)
        {
            //TODO - sprawdzic jak to sprawdzic:
            //glowny problem takie ustawienie 
            //
            // D D D D D
            // D 
            // D 
            // D 

            //for (int x = 0; x < board.Size; x++)
            //{
            //    for (int y = 0; y < board.Size; y++)
            //    {
            //        if (board.Fields[x][y].Type == type)
            //    }
            //}
        }
    }
}
