namespace Chess.Core;

public interface IDisplayService
{
    public void Send(FenObject fen);

    public void Send(FenObject fen, Point point);
}
