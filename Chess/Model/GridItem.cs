using Chess.Model.Pieces;

namespace Chess.Model;

public class GridItem
{
    [Obsolete("Remove dependency on the console in the shared GridItem dto")]
    public ConsoleColor BackgroundConsoleColor { get; set; } = Constants.DefaultConsoleBackgroundColor;

    public Piece Piece { get; set; }
}
