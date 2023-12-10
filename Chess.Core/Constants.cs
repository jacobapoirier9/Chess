namespace Chess.Core;

public static class Constants
{
    public const string StartingFenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public static class CharacterCode
    {
        public const char BlackRook = 'r';
        public const char BlackKnight = 'n';
        public const char BlackBishop = 'b';
        public const char BlackQueen = 'q';
        public const char BlackKing = 'k';
        public const char BlackPawn = 'p';

        public const char WhiteRook = 'R';
        public const char WhiteKnight = 'N';
        public const char WhiteBishop = 'B';
        public const char WhiteQueen = 'Q';
        public const char WhiteKing = 'K';
        public const char WhitePawn = 'P';
    }

    [Obsolete]
    public const char RookDisplayCharacter = 'R';

    [Obsolete]
    public const char KnightDisplayCharacter = 'N';

    [Obsolete]
    public const char BishopDisplayCharacter = 'B';

    [Obsolete]
    public const char QueenDisplayCharacter = 'Q';

    [Obsolete]
    public const char KingDisplayCharacter = 'K';

    [Obsolete]
    public const char PawnDisplayCharacter = 'P';

    [Obsolete]
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