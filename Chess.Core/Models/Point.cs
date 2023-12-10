using System.Diagnostics.CodeAnalysis;

namespace Chess.Core.Models;

public readonly struct Point
{
    public override string ToString() => $"({_row}, {_column})";

    public bool Equals(Point other) => _row == other.Row && _column == other.Column;

    private readonly int _row;
    private readonly int _column;

    public Point(int row, int column)
    {
        _row = row;
        _column = column;
    }

    public int Row => _row;
    public int Column => _column;

    public static bool operator ==(Point left, Point right) => left.Row == right.Row && left.Column == right.Column;
    public static bool operator !=(Point left, Point right) => left.Row != right.Row || left.Column != right.Column;

    public override int GetHashCode() => this.GenerateHashCode();

    public override bool Equals([NotNullWhen(true)] object obj) => Row == ((Point)obj).Row && Column == ((Point)obj).Column;
}