using Chess.Core.Models;

namespace Chess.Core;

public static class PointMapping
{
    // TODO: There's got to be a better way of handling this..
    public static Point ToPoint(string friendlyCoords)
    {
        var column = FromFriendlyColumn(Convert.ToChar(friendlyCoords[0]));
        var row = FromFriendlyRow(Convert.ToChar(friendlyCoords[1]));

        return new Point(row, column);
    }

    // TODO: There's got to be a better way of handling this..
    public static string FromPoint(Point point)
    {
        var column = ToFriendlyColumn(point.Column);
        var row = ToFriendlyRow(point.Row);

        return $"{column}{row}";
    }

    public static char ToFriendlyRow(int row)
    {
        return row switch
        {
            0 => '8',
            1 => '7',
            2 => '6',
            3 => '5',
            4 => '4',
            5 => '3',
            6 => '2',
            7 => '1',

            _ => throw new IndexOutOfRangeException()
        };
    }

    public static char ToFriendlyColumn(int column)
    {
        return column switch
        {
            0 => 'a',
            1 => 'b',
            2 => 'c',
            3 => 'd',
            4 => 'e',
            5 => 'f',
            6 => 'g',
            7 => 'h',

            _ => throw new IndexOutOfRangeException()
        };
    }

    public static int FromFriendlyRow(char row)
    {
        return row switch
        {
            '8' => 0,
            '7' => 1,
            '6' => 2,
            '5' => 3,
            '4' => 4,
            '3' => 5,
            '2' => 6,
            '1' => 7,

            _ => throw new IndexOutOfRangeException()
        };
    }

    public static int FromFriendlyColumn(char column)
    {
        return column switch
        {
            'a' => 0,
            'b' => 1,
            'c' => 2,
            'd' => 3,
            'e' => 4,
            'f' => 5,
            'g' => 6,
            'h' => 7,

            _ => throw new IndexOutOfRangeException()
        };
    }
}
