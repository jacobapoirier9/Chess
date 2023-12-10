using Chess.Core.Models;

namespace Chess.Core.Services;

public interface IPlayer
{
    public Point GetPieceSelectionPoint(FenObject fen);

    public Point GetPieceMovementSelectionPoint(FenObject fen);
}
