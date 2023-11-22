namespace Chess.Core;

public struct Point
{
    public override string ToString() => $"({_row}, {_column})";

    private readonly int _row;
    private readonly int _column;

    public Point(int row, int column)
    {
        _row = row;
        _column = column;
    }

    public int Row => _row;
    public int Column => _column;
}