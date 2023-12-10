using Chess.Core.Models;

namespace Chess.Core;

public static class Helper
{
    // https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode
    public static int GenerateHashCode<T>(this T item)
    {
        unchecked
        {
            var hash = 17;
            var mult = 23;

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(item);
                hash = hash * mult + value.GetHashCode();
            }

            return hash;
        }
    }

    public static bool IsBetweenInclusive(this int number, int low, int high) => low <= number && number <= high;

    public static void ForceSwap<T>(this T[,] grid, Point fromPoint, Point toPoint)
    {
        var from = grid[fromPoint.Row, fromPoint.Column];
        var to = grid[toPoint.Row, toPoint.Column];

        grid[fromPoint.Row, fromPoint.Column] = to;
        grid[toPoint.Row, toPoint.Column] = from;
    }


    public static T GetItemOrDefault<T>(this T[,] array, Point point)
    {
        if (array.CheckValidBounds(point))
            return array[point.Row, point.Column];

        return default;
    }

    public static T GetItem<T>(this T[,] array, Point point)
    {
        var item = array.GetItemOrDefault(point);

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