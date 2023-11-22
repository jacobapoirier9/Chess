using Chess.Core;
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

    private Point GetNextPoint(string prompt)
    {
        while (true)
        {
            var input = _getInputs.GetInput(prompt);

            var row = int.Parse(input.ElementAt(0).ToString());
            var column = int.Parse(input.ElementAt(1).ToString());

            if (row.IsBetween(0, Constants.GridSize - 1) && column.IsBetween(0, Constants.GridSize - 1))
            {
                return new Point(row, column);
            }
        }
    }

    public Point GetPieceSelectionPoint(FenObject fen) => GetNextPoint("Select which piece you would like to move (in format [RC]) >> ");
    public Point GetPieceMovementSelectionPoint(FenObject fen) => GetNextPoint("Select which move you would like to take (in format [RC]) >> ");
}
