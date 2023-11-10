using Chess.Enums;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess;

public static class Grid
{
    public const int Size = 8;
    
    public static GridItem[,] Create()
    {
        var grid = new GridItem[Size, Size];

        var rook = Constants.RookDisplayCharacter;
        var knight = Constants.KnightDisplayCharacter;
        var bishop = Constants.BishopDisplayCharacter;
        var queen = Constants.QueenDisplayCharacter;
        var king = Constants.KingDisplayCharacter;
        var pawn = Constants.PawnDisplayCharacter;
        var blank = Constants.DefaultDisplayCharacter;

        var temp = new char[,]
        {
            { rook, knight, bishop, queen, king, bishop, knight, rook },
            { pawn, pawn, pawn, pawn, pawn, pawn, pawn, pawn },

            { blank, blank, blank, blank, blank, blank, blank, blank },
            { blank, blank, blank, blank, blank, blank, blank, blank },
            { blank, blank, blank, blank, blank, blank, blank, blank },
            { blank, blank, blank, blank, blank, blank, blank, blank },

            { pawn, pawn, pawn, pawn, pawn, pawn, pawn, pawn },
            { rook, knight, bishop, queen, king, bishop, knight, rook },
        };

        for (var row = 0; row < Size; row++)
        {
            for (var col = 0; col < Size; col++)
            {
                var item = new GridItem();

                Player? player;
                if (row == 0 || row == 1)
                {
                    player = Player.White;
                }
                else if (row == 6 || row == 7)
                {
                    player = Player.Black;
                }
                else
                {
                    grid[row, col] = item;
                    continue;
                }

                item.Piece = temp[row, col] switch
                {
                    Constants.RookDisplayCharacter => new Rook(player.Value),
                    Constants.KnightDisplayCharacter => new Knight(player.Value),
                    Constants.BishopDisplayCharacter => new Bishop(player.Value),
                    Constants.QueenDisplayCharacter => new Queen(player.Value),
                    Constants.KingDisplayCharacter => new King(player.Value),
                    Constants.PawnDisplayCharacter => new Pawn(player.Value),

                    _ => throw new IndexOutOfRangeException(temp[row, col].ToString())
                };

                grid[row, col] = item;
            }
        }

        return grid;
    }

    public static void ForceSwap<T>(this T[,] array, int fromRow, int fromCol, int toRow, int toCol)
    {
        var from = array[fromRow, fromCol];
        var to = array[toRow, toCol];

        array[fromRow, fromCol] = to;
        array[toRow, toCol] = from;
    }

    public static T GetItemAtPositionOrDefault<T>(this T[,] array, int row, int col)
    {
        if (array.CheckValidPosition(row, col))
            return array[row, col];

        return default;
    }

    public static T GetItemAtPosition<T>(this T[,] array, int row, int col)
    {
        var item = GetItemAtPositionOrDefault(array, row, col);

        if (item is null)
            throw new IndexOutOfRangeException($"Invalid grid coordinates ({row}, {col})");

        return item;
    }

    public static bool CheckValidPosition<T>(this T[,] array, int row, int col) =>
        0 <= row && row < array.GetLength(0) && 0 <= col && col < array.GetLength(1);

    public static Player GetOtherPlayer(this Player player)
    {
        var other = player switch
        {
            Player.White => Player.Black,
            Player.Black => Player.White,

            _ => throw new IndexOutOfRangeException(nameof(player))
        };

        return other;
    }
}
