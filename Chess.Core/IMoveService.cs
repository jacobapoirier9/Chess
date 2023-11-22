namespace Chess.Core;

public interface IMoveService
{
    public List<Move> GenerateMoves(FenObject fen, Point point);
}
