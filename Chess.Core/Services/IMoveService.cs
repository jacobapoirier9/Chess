using Chess.Core.Models;

namespace Chess.Core.Services;

public interface IMoveService
{
    public List<Move> GenerateMoves(FenObject fen, Point point);

    public void ExecuteMove(FenObject fen, Point from, Point to, List<Move> moves);
}
