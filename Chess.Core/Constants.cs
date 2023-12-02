namespace Chess.Core;

public static class Constants
{
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

    public const int BlackPawnRow = 1;
    public const int WhitePawnRow = 6;
}