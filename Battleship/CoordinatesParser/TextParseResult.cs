namespace Battleship.CoordinatesParser
{
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
}
