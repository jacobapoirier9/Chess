using Chess.Model;

namespace Chess.Services;

public interface IDisplayService
{
    public void Send(GridItem[,] grid);

    public void ResetHighlights(GridItem[,] grid, ConsoleColor color = Constants.DefaultConsoleBackgroundColor);

    public void ApplyMoveHighlights(GridItem[,] grid, int row, int col);
}
