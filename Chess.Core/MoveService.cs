namespace Chess.Core;

public class MoveService : IMoveService
{
    public List<Move> GenerateMoves(FenObject fen, Point point)
    {
        var item = fen.Grid.GetItemAtPosition(point);

        var moves = new List<Move>();

        switch (item.CharacterCode)
        {
            case Constants.PawnDisplayCharacter:
                AddPawnMoves(fen, item, point, moves);
                break;

            case Constants.KingDisplayCharacter:
                AddKingMoves(fen, item, point, moves);
                break;

            case Constants.QueenDisplayCharacter:
                AddQueenMoves(fen, item, point, moves);
                break;

            case Constants.BishopDisplayCharacter:
                AddBishopMoves(fen, item, point, moves);
                break;

            case Constants.KnightDisplayCharacter:
                AddKnightMoves(fen, item, point, moves);
                break;

            case Constants.RookDisplayCharacter:
                AddRookMoves(fen, item, point, moves);
                break;
        }

        return moves;
    }

    private void AddCalculatedMove(FenObject fen, GridItem item, Point point, List<Move> moves, sbyte deltaRow, sbyte deltaColumn, int? maxSlide, bool allowAttack)
    {
        var grid = fen.Grid;
        for (var i = 1; i <= (maxSlide ?? Constants.GridSize); i++)
        {
            var targetRow = point.Row + deltaRow * i;
            var targetColumn = point.Column + deltaColumn * i;

            var targetPoint = new Point(targetRow, targetColumn);

            if (grid.CheckValidBounds(targetPoint))
            {
                var target = grid.GetItemAtPositionOrDefault(targetPoint);

                if (target is not null && target.Player == item.Player)
                    break;

                var move = new Move
                {
                    From = point,
                    To = targetPoint,
                };

                if (target is not null && target.Player == item.Player?.GetOtherPlayer())
                {
                    if (allowAttack)
                    {
                        move.IsAttack = true;
                        moves.Add(move);
                    }

                    break;
                }

                moves.Add(move);
            }
            else
            {
                break;
            }
        }
    }

    private void AddPawnMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        switch (item.Player)
        {
            case Player.Black:
                AddCalculatedMove(fen, item, point, moves, 1, 0, 1, false);
                break;

            case Player.White:
                AddCalculatedMove(fen, item, point, moves, -1, 0, 1, false);
                break;
        }
    }

    private void AddKingMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(fen, item, point, moves, 1, 1, 1, true);
        AddCalculatedMove(fen, item, point, moves, 1, 0, 1, true);
        AddCalculatedMove(fen, item, point, moves, 1, -1, 1, true);

        AddCalculatedMove(fen, item, point, moves, -1, 1, 1, true);
        AddCalculatedMove(fen, item, point, moves, -1, 0, 1, true);
        AddCalculatedMove(fen, item, point, moves, -1, -1, 1, true);

        AddCalculatedMove(fen, item, point, moves, 0, 1, 1, true);
        AddCalculatedMove(fen, item, point, moves, 0, -1, 1, true);
    }

    private void AddQueenMoves(FenObject fen, GridItem item, Point point,List<Move> moves)
    {
        AddCalculatedMove(fen, item, point, moves, 1, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, 1, 0, null, true);
        AddCalculatedMove(fen, item, point, moves, 1, -1, null, true);

        AddCalculatedMove(fen, item, point, moves, -1, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, -1, 0, null, true);
        AddCalculatedMove(fen, item, point, moves, -1, -1, null, true);

        AddCalculatedMove(fen, item, point, moves, 0, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, 0, -1, null, true);
    }

    private void AddBishopMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(fen, item, point, moves, 1, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, 1, -1, null, true);
        AddCalculatedMove(fen, item, point, moves, -1, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, -1, -1, null, true);
    }

    private void AddKnightMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(fen, item, point, moves, 1, 2, 1, true);
        AddCalculatedMove(fen, item, point, moves, 1, -2, 1, true);
        AddCalculatedMove(fen, item, point, moves, -1, 2, 1, true);
        AddCalculatedMove(fen, item, point, moves, -1, -2, 1, true);

        AddCalculatedMove(fen, item, point, moves, 2, 1, 1, true);
        AddCalculatedMove(fen, item, point, moves, 2, -1, 1, true);
        AddCalculatedMove(fen, item, point, moves, -2, 1, 1, true);
        AddCalculatedMove(fen, item, point, moves, -2, -1, 1, true);
    }

    private void AddRookMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(fen, item, point, moves, 1, 0, null, true);
        AddCalculatedMove(fen, item, point, moves, -1, 0, null, true);
        AddCalculatedMove(fen, item, point, moves, 0, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, 0, -1, null, true);
    }
}
