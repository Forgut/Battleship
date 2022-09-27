using System.Linq;
using System.Text.RegularExpressions;

namespace Battleship.CoordinatesParser
{
    public class DefaultCoordinatesParser : ICoordinatesParser
    {
        private readonly int _gameSize;

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

            var findLetterRegex = @"[A-Z]";
            var findNumberRegex = @"[0-9]+";

            try
            {
                var letter = Regex.Match(text, findLetterRegex).Value.First();
                var row = (int)letter - 65;
                var number = Regex.Match(text, findNumberRegex).Value;
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
}
