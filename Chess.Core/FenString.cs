public interface IFenStringService
{
    public void Parse(string fen);
}

public class FenStringService : IFenStringService
{
    public void Parse(string fen)
    {
        var parts = fen.Split(' ');
    }

    private void ParsePiecePlacement(string part)
    {
        var sub = part.Split('/');
        for (var row = 0; row < sub.Length; row++)
        {
            var col = 0;

            var next = sub[row];

            
        }
    }
}

public struct GridItem
{
    public int Row { get; set; }

    public int Column { get; set; }
}