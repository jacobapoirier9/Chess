using Chess.Enums;
using Chess.Model;
using System.Drawing;

namespace Chess.Services;

public class ConsoleDisplayService : IDisplayService
{
    private static bool _preserveConsole = true;

    private readonly IMoveService _moveService;
    public ConsoleDisplayService(IMoveService moveService)
    {
        _moveService = moveService;
    }

    public void Send(GridItem[,] grid)
    {
        if (!_preserveConsole)
            Console.Clear();

        Console.BackgroundColor = Constants.DefaultConsoleBackgroundColor;
        Console.WriteLine();
        Console.WriteLine($"   | 0  1  2  3  4  5  6  7 ");
        Console.WriteLine($"---+------------------------");

        for (var row = 0; row < Grid.Size; row++)
        {
            Console.BackgroundColor = Constants.DefaultConsoleBackgroundColor;
            Console.Write(" {0} |", row);

            for (var col = 0; col < Grid.Size; col++)
            {
                var item = grid[row, col];

                Console.BackgroundColor = item.BackgroundConsoleColor;

                if (item.Piece is not null)
                {
                    Console.Write(item.Piece.CharacterCode);
                    Console.Write((int)item.Piece.Player);
                    Console.Write(Constants.DefaultDisplayCharacter);
                }
                else
                {
                    Console.Write("   ");
                }
            }

            Console.BackgroundColor = Constants.DefaultConsoleBackgroundColor;
            Console.WriteLine();
        }

        if (_preserveConsole)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    public void ResetHighlights(GridItem[,] grid, ConsoleColor color = Constants.DefaultConsoleBackgroundColor)
    {
        for (var row = 0; row < Grid.Size; row++)
        {
            for (var col = 0; col < Grid.Size; col++)
            {
                grid[row, col].BackgroundConsoleColor = color;
            }
        }
    }

    public void ApplyMoveHighlights(GridItem[,] grid, int row, int col)
    {
        ResetHighlights(grid);

        var moves = _moveService.GetExpandedMoves(grid, row, col);
        var item = grid.GetItemAtPosition(row, col);

        item.BackgroundConsoleColor = ConsoleColor.Green;

        foreach (var move in moves)
        {
            var target = grid.GetItemAtPosition(move.TargetRow, move.TargetCol);
            if (move.MoveType == MoveType.PassiveOnly)
                target.BackgroundConsoleColor = ConsoleColor.Blue;
            else if (move.MoveType == MoveType.AttackOnly)
                target.BackgroundConsoleColor = ConsoleColor.Red;
            else
                target.BackgroundConsoleColor = ConsoleColor.Black;
        }
    }
}