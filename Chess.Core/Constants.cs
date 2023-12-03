namespace Chess.Core;

public static class Constants
{
    public const string StartingFenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public const char RookDisplayCharacter = 'R';
    public const char KnightDisplayCharacter = 'N';
    public const char BishopDisplayCharacter = 'B';
    public const char QueenDisplayCharacter = 'Q';
    public const char KingDisplayCharacter = 'K';
    public const char PawnDisplayCharacter = 'P';
    public const char DefaultDisplayCharacter = ' ';

    public const char FenStringSegmentSeparatorCharacter = ' ';
    public const char FenStringGridItemLineSeparatorCharacter = '/';
    public const char FenStringEmptySegmentCharacter = '-';

    public const int GridSize = 8;

    public const int BlackBaseLineRow = 0;
    public const int BlackPawnRow = 1;

    public const int WhiteBaseLineRow = 7;
    public const int WhitePawnRow = 6;

    public const int LeftRookStartingColumn = 0;
    public const int LeftKnightStartingColumn = 1;
    public const int LeftBishopStartingColumn = 2;
    public const int LeftQueenStartingColumn = 3;
    public const int RightKingStartingColumn = 4;
    public const int RightBishopStartingColumn = 5;
    public const int RightKnightStartingColumn = 6;
    public const int RightRookStartingColumn = 7;
}