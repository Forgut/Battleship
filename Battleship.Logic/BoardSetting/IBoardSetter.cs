using Battleship.Logic.Core;

namespace Battleship.Logic.BoardSetting
{
    public interface IBoardSetter
    {
        void SetupBoard(ISettable board);
    }
}
