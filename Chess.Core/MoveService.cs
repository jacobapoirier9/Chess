namespace Chess.Core;

public class MoveService : IMoveService
{
    public List<Move> GenerateMoves(GridItem[,] grid, Point point)
    {
        var item = grid.GetItemAtPosition(point);

        var moves = new List<Move>();

        switch (item.CharacterCode)
        {
            case Constants.PawnDisplayCharacter:
                AddPawnMoves(grid, item, point, moves);
                break;

            case Constants.KingDisplayCharacter:
                AddKingMoves(grid, item, point, moves);
                break;

            case Constants.QueenDisplayCharacter:
                AddQueenMoves(grid, item, point, moves);
                break;

            case Constants.BishopDisplayCharacter:
                AddBishopMoves(grid, item, point, moves);
                break;

            case Constants.KnightDisplayCharacter:
                AddKnightMoves(grid, item, point, moves);
                break;

            case Constants.RookDisplayCharacter:
                AddRookMoves(grid, item, point, moves);
                break;
        }

        return moves;
    }

    private void AddCalculatedMove(GridItem[,] grid, GridItem item, Point point, List<Move> moves, sbyte deltaRow, sbyte deltaColumn, int? maxSlide, bool allowAttack)
    {
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

    private void AddPawnMoves(GridItem[,] grid, GridItem item, Point point, List<Move> moves)
    {
        switch (item.Player)
        {
            case Player.Black:
                AddCalculatedMove(grid, item, point, moves, 1, 0, 1, false);
                break;

            case Player.White:
                AddCalculatedMove(grid, item, point, moves, -1, 0, 1, false);
                break;
        }
    }

    private void AddKingMoves(GridItem[,] grid, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(grid, item, point, moves, 1, 1, 1, true);
        AddCalculatedMove(grid, item, point, moves, 1, 0, 1, true);
        AddCalculatedMove(grid, item, point, moves, 1, -1, 1, true);

        AddCalculatedMove(grid, item, point, moves, -1, 1, 1, true);
        AddCalculatedMove(grid, item, point, moves, -1, 0, 1, true);
        AddCalculatedMove(grid, item, point, moves, -1, -1, 1, true);

        AddCalculatedMove(grid, item, point, moves, 0, 1, 1, true);
        AddCalculatedMove(grid, item, point, moves, 0, -1, 1, true);
    }

    private void AddQueenMoves(GridItem[,] grid, GridItem item, Point point,List<Move> moves)
    {
        AddCalculatedMove(grid, item, point, moves, 1, 1, null, true);
        AddCalculatedMove(grid, item, point, moves, 1, 0, null, true);
        AddCalculatedMove(grid, item, point, moves, 1, -1, null, true);

        AddCalculatedMove(grid, item, point, moves, -1, 1, null, true);
        AddCalculatedMove(grid, item, point, moves, -1, 0, null, true);
        AddCalculatedMove(grid, item, point, moves, -1, -1, null, true);

        AddCalculatedMove(grid, item, point, moves, 0, 1, null, true);
        AddCalculatedMove(grid, item, point, moves, 0, -1, null, true);
    }

    private void AddBishopMoves(GridItem[,] grid, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(grid, item, point, moves, 1, 1, null, true);
        AddCalculatedMove(grid, item, point, moves, 1, -1, null, true);
        AddCalculatedMove(grid, item, point, moves, -1, 1, null, true);
        AddCalculatedMove(grid, item, point, moves, -1, -1, null, true);
    }

    private void AddKnightMoves(GridItem[,] grid, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(grid, item, point, moves, 1, 2, 1, true);
        AddCalculatedMove(grid, item, point, moves, 1, -2, 1, true);
        AddCalculatedMove(grid, item, point, moves, -1, 2, 1, true);
        AddCalculatedMove(grid, item, point, moves, -1, -2, 1, true);

        AddCalculatedMove(grid, item, point, moves, 2, 1, 1, true);
        AddCalculatedMove(grid, item, point, moves, 2, -1, 1, true);
        AddCalculatedMove(grid, item, point, moves, -2, 1, 1, true);
        AddCalculatedMove(grid, item, point, moves, -2, -1, 1, true);
    }

    private void AddRookMoves(GridItem[,] grid, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(grid, item, point, moves, 1, 0, null, true);
        AddCalculatedMove(grid, item, point, moves, -1, 0, null, true);
        AddCalculatedMove(grid, item, point, moves, 0, 1, null, true);
        AddCalculatedMove(grid, item, point, moves, 0, -1, null, true);
    }
}
