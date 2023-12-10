using Chess.Core.Models;

namespace Chess.Core;

public static class Grid
{
    public static void ForceSwap(this GridItem[,] grid, Point fromPoint, Point toPoint)
    {
        var from = grid[fromPoint.Row, fromPoint.Column];
        var to = grid[toPoint.Row, toPoint.Column];

        grid[fromPoint.Row, fromPoint.Column] = to;
        grid[toPoint.Row, toPoint.Column] = from;
    }


    public static T GetItemAtPositionOrDefault<T>(this T[,] array, Point point)
    {
        if (array.CheckValidBounds(point))
            return array[point.Row, point.Column];

        return default;
    }

    public static T GetItemAtPosition<T>(this T[,] array, Point point)
    {
        var item = array.GetItemAtPositionOrDefault(point);

        if (item is null)
            throw new IndexOutOfRangeException($"Invalid grid coordinates ({point.Row}, {point.Column})");

        return item;
    }

    public static bool CheckValidBounds<T>(this T[,] array, Point point) =>
        0 <= point.Row && point.Row < array.GetLength(0) && 0 <= point.Column && point.Column < array.GetLength(1);

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
