using System.ComponentModel.DataAnnotations.Schema;

namespace Chess;

public class GridItem
{
    public override string ToString() => $"{Player} {CharacterCode} ({Row}, {Column})";

    public char? CharacterCode { get; set; }

    public Player? Player { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }
}