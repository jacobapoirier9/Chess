namespace Chess.Core;

public interface IMoveService
{
    public List<Move> GenerateMoves(FenObject fen, Point point);

    public void ExecuteMove(FenObject fen, Point from, Point to, List<Move> moves);
}
