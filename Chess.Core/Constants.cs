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

    public const int BlackBaseLineRow = 0;
    public const int BlackPawnRow = 1;

    public const int WhiteBaseLineRow = 7;
    public const int WhitePawnRow = 6;

    public const int LeftRookColumn = 0;
    public const int LeftKnightColumn = 1;
    public const int LeftBishopColumn = 2;
    public const int LeftQueenColumn = 3;
    public const int RightKingColumn = 4;
    public const int RightBishopColumn = 5;
    public const int RightKnightColumn = 6;
    public const int RightRookColumn = 7;
}