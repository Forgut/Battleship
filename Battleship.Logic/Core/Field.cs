using Battleship.Logic.Core.Enums;

namespace Battleship.Logic.Core
{
    public class Field
    {
        public Field(EFieldType type, bool isHit, int? shipId)
        {
            Type = type;
            IsHit = isHit;
            ShipId = shipId;
        }

        public int? ShipId { get; internal set; }
        public EFieldType Type { get; internal set; }
        public bool IsHit { get; internal set; }
    }
}
