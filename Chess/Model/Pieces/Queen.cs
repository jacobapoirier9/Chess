using Chess.Enums;
using Chess.Model;

namespace Chess.Model.Pieces;

public class Queen : Piece
{
    public Queen(Player player) : base(player, Constants.QueenDisplayCharacter)
    {
        AvailableMoves = new List<TemplateMove>()
        {
            CreatePassiveAndAttackMove(1, Direction.Positive, 1, Direction.Positive),
            CreatePassiveAndAttackMove(1, Direction.Positive, 1, Direction.Negative),
            CreatePassiveAndAttackMove(1, Direction.Negative, 1, Direction.Positive),
            CreatePassiveAndAttackMove(1, Direction.Negative, 1, Direction.Negative),

            CreatePassiveAndAttackMove(1, Direction.Positive, 0, Direction.Neutral),
            CreatePassiveAndAttackMove(1, Direction.Negative, 0, Direction.Neutral),
            CreatePassiveAndAttackMove(0, Direction.Neutral, 1, Direction.Positive),
            CreatePassiveAndAttackMove(0, Direction.Neutral, 1, Direction.Negative),
        };
    }

    public static TemplateMove CreatePassiveAndAttackMove(int moveRows, Direction rowDirection, int moveCols, Direction colDirection)
    {
        var move = new TemplateMove
        {
            AllowHopOver = false,
            AllowSlide = true,
            MaxSlideCount = Grid.Size,
            MoveType = MoveType.PassiveOrAttack,
            RemoveAfterUse = false,

            MoveRows = moveRows,
            RowDirection = rowDirection,
            MoveCols = moveCols,
            ColDirection = colDirection,
        };

        return move;
    }
}
