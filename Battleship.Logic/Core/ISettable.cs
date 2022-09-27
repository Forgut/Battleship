using Battleship.Logic.Core.Enums;

namespace Battleship.Logic.Core
{
    public interface ISettable
    {
        void SetFieldType(int x, int y, EFieldType type, int? shipId);
        int Size { get; }
    }
}
