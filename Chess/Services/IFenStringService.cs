namespace Chess.Services;

public interface IFenStringService
{
    public void Parse(string fen);

    public GridItem[,] ParseGrid(string piecePlacementSegment);
}
