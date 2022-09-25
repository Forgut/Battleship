using Battleship.Logic;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 10;
            var board = new Board(size);
            var setup = new BoardSetter();
            setup.SetupBoard(board);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    //Console.Write($"{i}{j} ");
                    var field = board.Fields[i][j];
                    if (field.IsHit)
                        Console.Write("X ");
                    else
                        switch (field.Type)
                        {
                            case EFieldType.Water:
                                Console.Write("  ");
                                break;
                            case EFieldType.Battleship:
                                Console.Write("B ");
                                break;
                            case EFieldType.Destroyer:
                                Console.Write("D ");
                                break;
                        }
                }
                Console.WriteLine();
            }
        }
    }
}
