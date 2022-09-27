using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic.ShootingLogic
{
    public class OneFieldBoardShooter : IBoardShooter
    {
        public EHitResult ShootAtPosition(IHittable board, int row, int column)
        {
            return board.MarkFieldAsHit(row, column);
        }
    }
}
