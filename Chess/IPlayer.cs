using Chess.Model;
using Chess.Model.Pieces;
using CommandSurfacer.Services;

namespace Chess;

public interface IPlayer
{
    public Piece GetPieceSelection(GridItem[,] grid);

    public void GetMoveSelection(GridItem[,] grid, List<ExpandedMove> moves);

    public void ExecuteMove(GridItem[,] grid, ExpandedMove move);
}
