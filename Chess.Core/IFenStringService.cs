namespace Chess.Core;

public interface IFenStringService
{
    public FenObject Parse(string fen);

    public GridItem[,] ParsePiecePlacement(string piecePlacementSegment);

    public Player ParseActiveColor(string activeColorSegment);

    public List<CastlingRight> ParseCastlingRights(string castlingRightsSegment);

    public Point? ParseEnPassantTarget(string possibleEnPassantTargetsSegment);

    public int ParseHalfMoveClock(string halfmoveClockSegment);

    public int ParseFullMoveNumber(string fullmoveNumberSegment);


    public string Generate(FenObject fen);

    public string GeneratePiecePlacementSegment(GridItem[,] grid);
}
