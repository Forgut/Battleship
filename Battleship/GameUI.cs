using Battleship.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class GameUI
    {
        private Game _game;

        private const string WATER_FIELD = "~";
        private const string BATTLESHIP_FIELD = "B";
        private const string DESTROYER_FIELD = "D";

        public GameUI(Game game)
        {
            _game = game;
        }

        public void PrintUI()
        {
            Console.Clear();

            PrintTurnMark();

            PrintPlayerBoard();
            PrintComputerBoard();

            PrintCommandLine();
        }
        private void PrintTurnMark()
        {
            WriteInColor($"Turn: {_game.Turn}", ConsoleColor.White);
            Console.WriteLine();
        }

        private void PrintPlayerBoard()
        {
            Console.WriteLine("### Your board ###");
            WriteColumnLetters();
            Console.WriteLine();

            for (int x = 0; x < _game.Size; x++)
            {
                WriteRowNumber(x + 1);

                for (int y = 0; y < _game.Size; y++)
                {
                    var field = _game.PlayerBoard.Fields[x][y];
                    var (text, color) = GetColoredTextDependingOnTypeAndHit(field);
                    WriteInColor($"{text} ", color);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintComputerBoard()
        {
            Console.WriteLine("### Computer board ###");
            WriteColumnLetters();
            Console.WriteLine();

            for (int x = 0; x < _game.Size; x++)
            {
                WriteRowNumber(x + 1);

                for (int y = 0; y < _game.Size; y++)
                {
                    var field = _game.ComputerBoard.Fields[x][y];
                    var (text, color) = GetColoredTextDependingOnTypeAndHit(field);
                    WriteInColor($"{text} ", color);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintCommandLine()
        {
            if (_game.Turn == ETurn.Player)
                WriteInColor("Type field you want to shoot at:\n", ConsoleColor.White);
            WriteInColor("Computer is calculating where to shoot...\n", ConsoleColor.White);
        }

       
        private void WriteColumnLetters()
        {
            WriteInColor("  ", ConsoleColor.Black);
            for (int i = 0; i < _game.Size; i++)
                WriteInColor($"{MapRowIndexToNumber(i)} ", ConsoleColor.Magenta);
        }

        private void WriteRowNumber(int row)
            => WriteInColor(row.ToString().PadLeft(2, ' '), ConsoleColor.Magenta);

        private char MapRowIndexToNumber(int index)
        {
            return (char)(index + 65);
        }


        private (string Text, ConsoleColor Color) GetColoredTextDependingOnTypeAndHit(Field field)
        {
            return (GetText(), GetColor());

            string GetText()
            {
                switch (field.Type)
                {
                    case EFieldType.Water:
                        return WATER_FIELD;
                    case EFieldType.Battleship:
                        return field.ShipId.ToString();
                    case EFieldType.Destroyer:
                        return field.ShipId.ToString();
                }
                return null;
            }

            ConsoleColor GetColor()
            {
                if (field.IsHit)
                {
                    switch (field.Type)
                    {
                        case EFieldType.Water:
                            return ConsoleColor.White;
                        case EFieldType.Battleship:
                        case EFieldType.Destroyer:
                            return ConsoleColor.Red;
                    }
                }

                switch (field.Type)
                {
                    case EFieldType.Water:
                        return ConsoleColor.Cyan;
                    case EFieldType.Battleship:
                    case EFieldType.Destroyer:
                        return ConsoleColor.Gray;
                }

                return ConsoleColor.Black;
            }
        }

        private void WriteInColor(string text, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = previousColor;
        }
    }
}
