using Chess.Core;
using CommandSurfacer.Services;

namespace Chess;

public class MemoryInputPlayer : IPlayer, IPlayerOne, IPlayerTwo
{
    private readonly IGetInput _getInputs;

    public MemoryInputPlayer(params object[] responses)
    {
        _getInputs = new GetMemoryInput(responses);
    }

    private Point GetNextPoint()
    {
        var input = _getInputs.GetInput(null);

        var row = int.Parse(input.ElementAt(0).ToString());
        var column = int.Parse(input.ElementAt(1).ToString());

        if (!row.IsBetweenInclusive(0, Constants.GridSize - 1) || !column.IsBetweenInclusive(0, Constants.GridSize - 1))
        {
            throw new IndexOutOfRangeException("Index out of bounds");
        }

        return new Point(row, column);
    }

    public Point GetPieceSelectionPoint(FenObject fen) => GetNextPoint();
    public Point GetPieceMovementSelectionPoint(FenObject fen) => GetNextPoint();
}
