namespace Chess;

public class Move
{
    public override string ToString() => $"({FromRow}, {FromColumn}) -> ({ToRow}, {ToColumn})";

    public int FromRow { get; set; }

    public int FromColumn { get; set; }

    public int ToRow { get; set; }

    public int ToColumn { get; set; }

    public bool IsAttack { get; set; }
}
