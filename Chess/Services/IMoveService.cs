using Chess.Model;

namespace Chess.Services;

public interface IMoveService
{
    public List<ExpandedMove> GetExpandedMoves(GridItem[,] grid, int row, int col);

    public bool CanMove(GridItem[,] grid, ExpandedMove move);

    public void ExecuteMove(GridItem[,] grid, ExpandedMove move);
}
