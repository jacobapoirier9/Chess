﻿using Chess.Core;
using Chess.Core.Models;
using Chess.Core.Services;

namespace Chess;

public class ConsoleDisplayService : IDisplayService
{
    private static bool _preserveConsole = true;

    private const ConsoleColor _defaultConsoleColor = ConsoleColor.Black;

    private void SendCore(FenObject fen, Point? point, List<Move> moves)
    {
        if (!_preserveConsole)
            Console.Clear();

        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = _defaultConsoleColor;
        Console.WriteLine();
        Console.WriteLine($"   | a  b  c  d  e  f  g  h ");
        Console.WriteLine($"---+------------------------");

        for (var row = 0; row < Constants.GridSize; row++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = _defaultConsoleColor;
            Console.Write(" {0} |", PointMapping.ToFriendlyRow(row));

            for (var column = 0; column < Constants.GridSize; column++)
            {
                var target = fen.Grid.GetItemOrDefault(new Point(row, column));

                Console.BackgroundColor = ConsoleColor.Black;

                if (point.HasValue && moves is not null)
                {
                    if (point.Value.Row == row && point.Value.Column == column)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;

                    var move = moves.SingleOrDefault(m => m.To.Row == row && m.To.Column == column);
                    if (move is not null)
                    {
                        if (move.IsAttack)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        }
                        else if (move.IsCastling)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                        }
                    }
                }

                if (target.HasValue)
                {
                    Console.ForegroundColor = char.IsLower(target.Value) ?  ConsoleColor.DarkGray : ConsoleColor.White;

                    Console.Write(' ');
                    Console.Write(target);
                    Console.Write(' ');
                }
                else
                {
                    Console.Write("   ");
                }
            }

            Console.BackgroundColor = _defaultConsoleColor;
            Console.WriteLine();
        }

        if (_preserveConsole)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    public void Draw(FenObject fen) =>
        SendCore(fen, null, null);

    public void Draw(FenObject fen, Point point, List<Move> moves) =>
        SendCore(fen, point, moves);
}