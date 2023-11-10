using Chess.Model;
using Chess.Model.Pieces;
using Chess.Services;
using CommandSurfacer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chess;

public class ConsoleInputPlayer : IPlayer, IPlayerOne, IPlayerTwo
{
    public IGetInput GetInputs => _getInputs;

    private readonly IGetInput _getInputs;
    private readonly IStringConverter _stringConverter;

    public ConsoleInputPlayer(IServiceProvider serviceProvider)
    {
        _stringConverter = serviceProvider.GetRequiredService<IStringConverter>();
        _getInputs = new GetConsoleInput(_stringConverter);
    }

    public Piece GetPieceSelection(GridItem[,] grid)
    {
        while (true)
        {
            var input = _getInputs.GetInput("Enter Selection: ");

            var row = _stringConverter.Convert<int>(input.ElementAt(0).ToString());
            var col = _stringConverter.Convert<int>(input.ElementAt(1).ToString());

            var item = grid.GetItemAtPositionOrDefault(row, col);
            if (item is null || item.Piece is null)
            {
                Console.WriteLine("Invalid Coordinates");
                continue;
            }

            Console.WriteLine("Selected: {0}", item.Piece.CharacterCode);

            return item.Piece;
        }
    }

    public void GetMoveSelection(GridItem[,] grid, List<ExpandedMove> moves)
    {
    }

    public void ExecuteMove(GridItem[,] grid, ExpandedMove move)
    {
    }
}
