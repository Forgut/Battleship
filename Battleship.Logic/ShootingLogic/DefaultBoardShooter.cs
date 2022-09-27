using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;

namespace Battleship.Logic.ShootingLogic
{
    public class DefaultBoardShooter : IBoardShooter
    {
        public EHitResult ShootAtPosition(IHittable board, int row, int column)
        {
            return board.MarkFieldAsHit(row, column);
        }
    }
}
