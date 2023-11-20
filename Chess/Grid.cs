namespace Chess;

public static class Grid
{
    public static void ForceSwap(this GridItem[,] array, int fromRow, int fromColumn, int toRow, int toColumn)
    {
        var from = array[fromRow, fromColumn];
        var to = array[toRow, toColumn];

        array[fromRow, fromColumn] = to;
        array[toRow, toColumn] = from;

        if (from is not null)
        {
            from.Row = toRow;
            from.Column = toColumn;
        }

        if (to is not null)
        {
            to.Row = fromRow;
            to.Column = fromColumn;
        }
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
