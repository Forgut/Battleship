using Battleship.Logic.Common.Validation;
using Battleship.Logic.Core.Enums;
using System.Linq;

namespace Battleship.Logic.Core
{
    public class Board : ISettable, IHittable
    {
        /* [x, y]
         * Y increases horizontaly
         * X increases vertically
         00 01 02 03 04 05 06 07 08 09
         10 11 12 13 14 15 16 17 18 19
         20 21 22 23 24 25 26 27 28 29
         30 31 32 33 34 35 36 37 38 39
         40 41 42 43 44 45 46 47 48 49
         50 51 52 53 54 55 56 57 58 59
         60 61 62 63 64 65 66 67 68 69
         70 71 72 73 74 75 76 77 78 79
         80 81 82 83 84 85 86 87 88 89
         90 91 92 93 94 95 96 97 98 99
         */

        private readonly Field[][] _fields;
        private readonly CoordinatesValidator _coordinatesValidator;

        public Board(int size)
        {
            _fields = new Field[size][];
            for (int i = 0; i < size; i++)
                _fields[i] = new Field[size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    _fields[x][y] = new Field(EFieldType.Water, isHit: false, shipId: null);
            Size = size;
            _coordinatesValidator = new CoordinatesValidator(size);
        }

        public Field[][] Fields => _fields;
        public int Size { get; }

        public void SetFieldType(int row, int column, EFieldType type, int? shipId)
        {
            if (!_coordinatesValidator.IsValid(row, column))
                return;

            _fields[column][row].Type = type;
            _fields[column][row].IsHit = false;
            _fields[column][row].ShipId = shipId;
        }

        public EHitResult MarkFieldAsHit(int row, int column)
        {
            if (!_coordinatesValidator.IsValid(row, column))
                return EHitResult.Miss;

            var field = _fields[column][row];
            field.IsHit = true;
            if (field.Type == EFieldType.Water)
                return EHitResult.Miss;

            if (_fields.Any(c => c.Any(f => f.ShipId == field.ShipId && !f.IsHit)))
                return EHitResult.Hit;

            return EHitResult.Sunk;
        }
    }
}
