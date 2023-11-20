namespace Chess.Services;

public interface IFenStringService
{
    public FenObject Parse(string fen);

    public GridItem[,] ParsePiecePlacement(string piecePlacementSegment);

    public Player ParseActiveColor(string activeColorSegment);

    public List<CastlingRight> ParseCastlingRights(string castlingRightsSegment);

    public (int row, int column)? ParseEnPassantTarget(string possibleEnPassantTargetsSegment);
}
