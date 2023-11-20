namespace Chess.Services;

public interface IMoveService
{
    public List<Move> GenerateMoves(GridItem[,] grid, Point point);
}
