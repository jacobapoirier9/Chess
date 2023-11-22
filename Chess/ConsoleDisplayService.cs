﻿using Chess.Core;

namespace Chess;

public class ConsoleDisplayService : IDisplayService
{
    private static bool _preserveConsole = true;

    private const ConsoleColor _defaultConsoleColor = ConsoleColor.Black;


    private readonly IMoveService _moveService;
    public ConsoleDisplayService(IMoveService moveService)
    {
        _moveService = moveService;
    }

    private void SendCore(FenObject fen, Point? point, List<Move> moves)
    {
        if (!_preserveConsole)
            Console.Clear();

        Console.BackgroundColor = _defaultConsoleColor;
        Console.WriteLine();
        Console.WriteLine($"   | 0  1  2  3  4  5  6  7 ");
        Console.WriteLine($"---+------------------------");

        for (var row = 0; row < Constants.GridSize; row++)
        {
            Console.BackgroundColor = _defaultConsoleColor;
            Console.Write(" {0} |", row);

            for (var column = 0; column < Constants.GridSize; column++)
            {
                var target = fen.Grid.GetItemAtPositionOrDefault(new Point(row, column));

                Console.BackgroundColor = ConsoleColor.Black;

                if (point.HasValue && moves is not null)
                {
                    if (point.Value.Row == row && point.Value.Column == column)
                        Console.BackgroundColor = ConsoleColor.Green;

                    var move = moves.SingleOrDefault(m => m.To.Row == row && m.To.Column == column);
                    if (move is not null)
                        Console.BackgroundColor = move.IsAttack ? ConsoleColor.Red : ConsoleColor.Blue;
                }

                if (target?.CharacterCode is not null)
                {
                    Console.Write(target.CharacterCode);
                    Console.Write((int)target.Player);
                    Console.Write(Constants.DefaultDisplayCharacter);
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

    public void Send(FenObject fen) =>
        SendCore(fen, null, null);

    public void Send(FenObject fen, Point point) =>
        SendCore(fen, point, _moveService.GenerateMoves(fen, point));
}