using Chess.Core.Models;

namespace Chess.Core.Services;

public interface IDisplayService
{
    public void Draw(FenObject fen);

    public void Draw(FenObject fen, Point point, List<Move> moves);
}
