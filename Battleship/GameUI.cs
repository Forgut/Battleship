using Battleship.ComputerShooters;
using Battleship.CoordinatesParser;
using Battleship.Logic.Core;
using Battleship.Logic.Core.Enums;
using System;

namespace Battleship
{
    public class GameUI
    {
        private readonly Game _game;
        private readonly ICoordinatesParser _coordinatesParser;
        private readonly IComputerShooter _computerShooter;

        private const string WATER_FIELD = "~";
        private const string BATTLESHIP_FIELD = "B";
        private const string DESTROYER_FIELD = "D";

        public bool DebugMode { get; set; }

        public GameUI(Game game, ICoordinatesParser coordinatesParser, IComputerShooter computerShooter)
        {
            _game = game;
            _coordinatesParser = coordinatesParser;
            _computerShooter = computerShooter;
        }

        public void PrintUI()
        {
            Console.Clear();

            PrintTurnMark();

            PrintPlayerBoard();

            if (DebugMode)
                PrintComputerBoardDebug();
            else
                PrintComputerBoard();
        }

        public EHitResult PrintAndExecuteOrder()
        {
            PrintCommandLine();
            if (_game.Turn == ETurn.Player)
                return HandlePlayerShot();
            else
                return HandleComputerShot();

            EHitResult HandleComputerShot()
            {
                var computerShot = _computerShooter.GetShotCoordinates();
                WriteInColor($"Computer shoots at {_coordinatesParser.Parse(computerShot.Row, computerShot.Column).Text}\n", ConsoleColor.Gray);
                var shotResult = _game.ShootAtPosition(computerShot.Row, computerShot.Column);
                PrintShotResult(shotResult);
                return shotResult;
            }

            EHitResult HandlePlayerShot()
            {
                TextParseResult result;
                do
                {
                    var order = Console.ReadLine();
                    result = _coordinatesParser.Parse(order);
                    if (!result.IsSuccess)
                        PrintErrorCommandLine();

                } while (!result.IsSuccess);

                var shotResult = _game.ShootAtPosition(result.Row, result.Column);
                PrintShotResult(shotResult);
                return shotResult;
            }
        }

        public void EndTurn()
        {
            WriteInColor("Press any key to proceed\n", ConsoleColor.Yellow);
            Console.ReadKey();
            _game.ToggleTurnMarker();
        }

        public EGameResult CheckGameState()
        {
            return _game.CheckGameState();
        }

        public void PrintAfterGameInfo()
        {
            var winner = _game.CheckGameState();
            if (winner == EGameResult.PlayerWins)
                WriteInColor("You won!\n", ConsoleColor.Green);
            else if (winner == EGameResult.ComputerWins)
                WriteInColor("You lost...\n", ConsoleColor.Red);
            else
                WriteInColor("Game should not be done yet... Something went wrong\n", ConsoleColor.Gray);
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
                    if (field.IsHit)
                    {
                        var (text, color) = GetColoredTextDependingOnTypeAndHit(field);
                        WriteInColor($"{text} ", color);
                    }
                    else
                    {
                        WriteInColor($"{WATER_FIELD} ", ConsoleColor.Blue);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintComputerBoardDebug()
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
            else
                WriteInColor("Computer is calculating where to shoot...\n", ConsoleColor.White);
        }

        private void PrintErrorCommandLine()
        {
            WriteInColor("Invalid input detected - type valid input (eg. A5)\n", ConsoleColor.Red);
        }

        private void PrintShotResult(EHitResult hitResult)
        {
            switch (hitResult)
            {
                case EHitResult.Miss:
                    WriteInColor("Its a miss!\n", ConsoleColor.Gray);
                    break;
                case EHitResult.Hit:
                    WriteInColor("Its a HIT!\n", ConsoleColor.DarkGreen);
                    break;
                case EHitResult.Sunk:
                    WriteInColor("Its a SINK!\n", ConsoleColor.Green);
                    break;
            }
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
                        return BATTLESHIP_FIELD;
                    case EFieldType.Destroyer:
                        return DESTROYER_FIELD;
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
                        return ConsoleColor.Blue;
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
