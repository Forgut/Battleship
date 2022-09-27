namespace Battleship.CoordinatesParser
{
    public interface ICoordinatesParser
    {
        TextParseResult Parse(string text);
        RowAndColumnParseResult Parse(int row, int column);
    }
}
