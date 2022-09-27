using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;

namespace Battleship.Logic.ShootingLogic
{
    public interface IBoardShooter
    {
        EHitResult ShootAtPosition(IHittable board, int x, int y);
    }
}
