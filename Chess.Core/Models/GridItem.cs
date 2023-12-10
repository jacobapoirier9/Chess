namespace Chess.Core.Models;

public class GridItem
{
    public override string ToString() => CharacterCode.ToString();

    public char? CharacterCode { get; set; }
}