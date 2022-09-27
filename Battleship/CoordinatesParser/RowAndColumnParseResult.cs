namespace Battleship.CoordinatesParser
{
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
