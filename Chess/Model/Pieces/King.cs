using Chess.Enums;
using Chess.Model;

namespace Chess.Model.Pieces;

public class King : Piece
{
    // TODO: The King has a very special first move. Can only preform if a Rook has not moved, and the King has a direct line of sight to the Rook. 
    public King(Player player) : base(player, Constants.KingDisplayCharacter)
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
