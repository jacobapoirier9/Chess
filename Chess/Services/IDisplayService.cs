namespace Chess.Services;

public interface IDisplayService
{
    public void Send(GridItem[,] grid);

    public void Send(GridItem[,] grid, Point point);
}
