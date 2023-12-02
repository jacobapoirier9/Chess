namespace Chess.Core;

public interface IFenStringService
{
    public FenObject ParseFenString(string fen);

    public GridItem[,] ParseGridSegment(string segment);

    public Player ParseActivePlayerSegment(string segment);

    public CastlingRights ParseCasltingRightsSegment(string segment);

    public Point? ParsePossibleEnPassantSegment(string segment);

    public int ParseHalfClockSegment(string segment);

    public int ParseFullMoveNumberSegment(string segment);


    public string GenerateFenString(FenObject fen);

    public string GenerateGridSegment(GridItem[,] grid);

    public string GenerateActivePlayerSegment(Player player);

    public string GenerateCastlingRightsSegment(FenObject fen);

    public string GeneratePossibleEnPassantSegment(FenObject fen)
}
