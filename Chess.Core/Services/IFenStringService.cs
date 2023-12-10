using Chess.Core.Models;

namespace Chess.Core.Services;

public interface IFenStringService
{
    public FenObject ParseFenString(string fen);

    public char?[,] ParseGridSegment(string segment);

    public Player ParseActivePlayerSegment(string segment);

    public CastlingRights ParseCasltingRightsSegment(string segment);

    public Point? ParsePossibleEnPassantSegment(string segment);

    public int ParseHalfClockSegment(string segment);

    public int ParseFullMoveNumberSegment(string segment);


    public string GenerateFenString(FenObject fen);

    public string GenerateGridSegment(FenObject fen);

    public string GenerateActivePlayerSegment(FenObject fen);

    public string GenerateCastlingRightsSegment(FenObject fen);

    public string GeneratePossibleEnPassantSegment(FenObject fen);

    public string GenerateHalfClockSegment(FenObject fen);

    public string GenerateFullMoveNumberSegment(FenObject fen);
}
