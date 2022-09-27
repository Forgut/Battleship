using Battleship.Logic.Core.Enums;

namespace Battleship.Logic.Core
{
    public interface IHittable
    {
        EHitResult MarkFieldAsHit(int x, int y);
    }
}
