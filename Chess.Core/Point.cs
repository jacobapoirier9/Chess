namespace Chess.Core;

public struct Point
{
    public override string ToString() => $"({_row}, {_column})";

    private readonly int _row;
    private readonly int _column;

    public Point(int row, int col)
    {
        _row = row;
        _column = col;
    }

    public int Row => _row;
    public int Column => _column;
}