namespace Chess.Core;

public interface IFenStringService
{
    public FenObject ParseFenString(string fen);

    public GridItem[,] ParseGridSegment(string piecePlacementSegment);

    public Player ParseActivePlayerSegment(string activeColorSegment);

    public List<CastlingRight> ParseCasltingRightsSegment(string castlingRightsSegment);

    public Point? ParsePossibleEnPassantSegment(string possibleEnPassantTargetsSegment);

    public int ParseHalfClockSegment(string halfmoveClockSegment);

    public int ParseFullMoveNumberSegment(string fullmoveNumberSegment);


    public string GenerateFenString(FenObject fen);

    public string GenerateGridSegment(GridItem[,] grid);

    public string GenerateActivePlayerSegment(Player player);
}
