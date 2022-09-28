namespace Battleship.Logic.Common.Validation
{
    public class CoordinatesValidator
    {
        private readonly int _gameSize;

        public CoordinatesValidator(int gameSize)
        {
            _gameSize = gameSize;
        }

        public bool IsValid(int row, int column)
        {
            if (row < 0 || row >= _gameSize || column < 0 || column >= _gameSize)
                return false;
            return true;
        }
    }
}
