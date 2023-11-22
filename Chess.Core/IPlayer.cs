namespace Chess.Core;

public interface IPlayer
{
    public Point GetPieceSelectionPoint(FenObject fen);

    public Point GetPieceMovementSelectionPoint(FenObject fen);
}
