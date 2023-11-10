using Chess.Enums;
using Chess.Model;

namespace Chess.Model.Pieces;

public class Knight : Piece
{
    public Knight(Player player) : base(player, Constants.KnightDisplayCharacter)
    {
        AvailableMoves = new List<TemplateMove>()
        {
            CreatePassiveAndAttackMove(1, Direction.Positive, 2, Direction.Positive),
            CreatePassiveAndAttackMove(1, Direction.Positive, 2, Direction.Negative),
            CreatePassiveAndAttackMove(1, Direction.Negative, 2, Direction.Positive),
            CreatePassiveAndAttackMove(1, Direction.Negative, 2, Direction.Negative),

            CreatePassiveAndAttackMove(2, Direction.Positive, 1, Direction.Positive),
            CreatePassiveAndAttackMove(2, Direction.Positive, 1, Direction.Negative),
            CreatePassiveAndAttackMove(2, Direction.Negative, 1, Direction.Positive),
            CreatePassiveAndAttackMove(2, Direction.Negative, 1, Direction.Negative),
        };
    }

    public static TemplateMove CreatePassiveAndAttackMove(int moveRows, Direction rowDirection, int moveCols, Direction colDirection)
    {
        var move = new TemplateMove
        {
            AllowHopOver = true,
            AllowSlide = false,
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
