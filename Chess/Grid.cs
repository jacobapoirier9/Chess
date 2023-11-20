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

        var temp = new char?[,]
        {
            { rook, knight, bishop, queen, king, bishop, knight, rook },
            { pawn, pawn, pawn, pawn, pawn, pawn, pawn, pawn },

            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },

            { pawn, pawn, pawn, pawn, pawn, pawn, pawn, pawn },
            { rook, knight, bishop, queen, king, bishop, knight, rook },
        };

        for (var row = 0; row < Size; row++)
        {
            for (var column = 0; column < Size; column++)
            {
                var item = new GridItem() { Row = row, Column = column, CharacterCode = temp[row, column] };

                if (row == 0 || row == 1)
                    item.Player = Player.White;
                else if (row == 6 || row == 7)
                    item.Player = Player.Black;

                grid[row, column] = item;
            }
        }

        return grid;
    }

    public static void ForceSwap(this GridItem[,] array, int fromRow, int fromColumn, int toRow, int toColumn)
    {
        var from = array[fromRow, fromColumn];
        var to = array[toRow, toColumn];

        array[fromRow, fromColumn] = to;
        array[toRow, toColumn] = from;

        from.Row = toRow;
        from.Column = toColumn;

        to.Row = fromRow;
        to.Column = fromColumn;
    }

    public static T GetItemAtPositionOrDefault<T>(this T[,] array, int row, int column)
    {
        if (array.CheckValidPosition(row, column))
            return array[row, column];

        return default;
    }

    public static T GetItemAtPosition<T>(this T[,] array, int row, int column)
    {
        var item = GetItemAtPositionOrDefault(array, row, column);

        if (item is null)
            throw new IndexOutOfRangeException($"Invalid grid coordinates ({row}, {column})");

        return item;
    }

    public static bool CheckValidPosition<T>(this T[,] array, int row, int column) =>
        0 <= row && row < array.GetLength(0) && 0 <= column && column < array.GetLength(1);

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
