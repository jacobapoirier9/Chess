namespace Chess.Core;

public interface IDisplayService
{
    public void Draw(FenObject fen);

    public void Draw(FenObject fen, Point point, List<Move> moves);
}
