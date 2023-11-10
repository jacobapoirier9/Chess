using Chess.Enums;
using Chess.Model;

namespace Chess.Model.Pieces;

public class Pawn : Piece
{
    public Pawn(Player player) : base(player, Constants.PawnDisplayCharacter)
    {
        AvailableMoves = new List<TemplateMove>
        {
            CreatePassiveFirstMove(player),
            CreatePassiveMove(player),
            CreateAttackMove(player, Direction.Positive),
            CreateAttackMove(player, Direction.Negative),
        };
    }

    public static Direction GetRowDirection(Player player)
    {
        var direction = player switch
        {
            Player.White => Direction.Positive,
            Player.Black => Direction.Negative,

            _ => throw new IndexOutOfRangeException(nameof(player))
        };

        return direction;
    }

    public static TemplateMove CreatePassiveFirstMove(Player player)
    {
        var move = new TemplateMove
        {
            AllowHopOver = false,
            AllowSlide = true,
            MaxSlideCount = 2,
            RemoveAfterUse = true,
            MoveType = MoveType.PassiveOnly,
            MoveRows = 1,
            RowDirection = GetRowDirection(player),
            MoveCols = 0,
            ColDirection = Direction.Neutral,
        };

        return move;
    }

    public static TemplateMove CreatePassiveMove(Player player)
    {
        var move = new TemplateMove
        {
            AllowHopOver = false,
            AllowSlide = false,
            RemoveAfterUse = false,
            MoveType = MoveType.PassiveOnly,
            MoveRows = 1,
            RowDirection = GetRowDirection(player),
            MoveCols = 0,
            ColDirection = Direction.Neutral,
        };

        return move;
    }

    public static TemplateMove CreateAttackMove(Player player, Direction colDirection)
    {
        var move = new TemplateMove
        {
            AllowHopOver = false,
            AllowSlide = false,
            RemoveAfterUse = false,
            MoveType = MoveType.AttackOnly,
            MoveRows = 1,
            RowDirection = GetRowDirection(player),
            MoveCols = 1,
            ColDirection = colDirection,
        };

        return move;
    }
}
