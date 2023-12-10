using Chess.Core;
using Chess.Core.Models;
using Chess.Core.Services;
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
        var point = PointMapping.ToPoint(input);

        if (!point.Row.IsBetweenInclusive(0, Constants.GridSize - 1) || !point.Column.IsBetweenInclusive(0, Constants.GridSize - 1))
        {
            throw new IndexOutOfRangeException("Index out of bounds");
        }

        return point;
    }

    public Point GetPieceSelectionPoint(FenObject fen) => GetNextPoint();
    public Point GetPieceMovementSelectionPoint(FenObject fen) => GetNextPoint();
}
