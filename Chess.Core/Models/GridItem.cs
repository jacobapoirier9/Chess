namespace Chess.Core.Models;

public class GridItem
{
    public override string ToString() => $"{Player} {CharacterCode}";

    public char? CharacterCode { get; set; }

    public Player? Player { get; set; }
}