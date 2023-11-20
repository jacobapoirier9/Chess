namespace Chess;

public class FenObject
{
    public GridItem[,] Grid { get; set; }

    public Player ActivePlayer { get; set; }

    public List<CastlingRight> CastlingRights { get; set; }

    public (int Row, int Column)? PossibleEnPassantTarget { get; set; }
}