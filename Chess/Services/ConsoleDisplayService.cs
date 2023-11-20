namespace Chess.Services;

public class ConsoleDisplayService : IDisplayService
{
    private static bool _preserveConsole = true;

    private readonly IMoveService _moveService;
    public ConsoleDisplayService(IMoveService moveService)
    {
        _moveService = moveService;
    }

    private void SendCore(GridItem[,] grid, GridItem item, List<Move> moves)
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

            for (var column = 0; column < Grid.Size; column++)
            {
                var target = grid.GetItemAtPosition(row, column);

                Console.BackgroundColor = ConsoleColor.Black;

                if (item is not null && moves is not null)
                {
                    if (item.Row == row && item.Column == column)
                        Console.BackgroundColor = ConsoleColor.Green;

                    var move = moves.SingleOrDefault(m => m.ToRow == row && m.ToColumn == column);
                    if (move is not null)
                        Console.BackgroundColor = move.IsAttack ? ConsoleColor.Red : ConsoleColor.Blue;
                }

                if (target.CharacterCode is not null)
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

    public void Send(GridItem[,] grid) => 
        SendCore(grid, null, null);

    public void Send(GridItem[,] grid, int row, int column) => 
        SendCore(grid, grid.GetItemAtPosition(row, column), _moveService.GenerateMoves(grid, row, column));
}