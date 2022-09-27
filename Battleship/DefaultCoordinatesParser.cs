using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleship
{
    public interface ICoordinatesParser
    {
        TextParseResult Parse(string text);
        RowAndColumnParseResult Parse(int row, int column);
    }

    public class DefaultCoordinatesParser : ICoordinatesParser
    {
        private int _gameSize;

        public DefaultCoordinatesParser(int gameSize)
        {
            _gameSize = gameSize;
        }

        /// <summary>
        /// Parses provided text in form [letter][number] into [X][Y] coordinates
        /// </summary>
        /// <param name="text"></param>
        public TextParseResult Parse(string text)
        {
            if (!ValidateInput(text))
                return new TextParseResult();

            try
            {
                var letter = Regex.Match(text, @"[A-Z]").Value.First();
                var row = (int)letter - 65;
                var number = Regex.Match(text, @"[0-9]+").Value;
                var column = int.Parse(number) - 1;

                if (!ValidateRowAndColumn(row, column))
                    return new TextParseResult();

                return new TextParseResult(row, column);
            }
            catch
            {
                return new TextParseResult();
            }
        }

        public RowAndColumnParseResult Parse(int row, int column)
        {
            if (!ValidateRowAndColumn(row, column))
                return new RowAndColumnParseResult();

            var letter = (char)(row + 65);
            var number = column + 1;
            return new RowAndColumnParseResult($"{letter}{number}");
        }

        private bool ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            if (!Regex.IsMatch(input, "^[A-Z]{1}[0-9]+"))
                return false;
            return true;
        }

        private bool ValidateRowAndColumn(int row, int column)
        {
            if (row < 0 || row >= _gameSize)
                return false;
            if (column < 0 || column >= _gameSize)
                return false;
            return true;
        }
    }

    public class TextParseResult
    {
        public TextParseResult()
        {
            IsSuccess = false;
        }

        public TextParseResult(int row, int column)
        {
            Row = row;
            Column = column;
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
    public class RowAndColumnParseResult
    {
        public RowAndColumnParseResult()
        {
            IsSuccess = false;
        }

        public RowAndColumnParseResult(string text)
        {
            Text = text;
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public string Text { get; set; }
    }
}
