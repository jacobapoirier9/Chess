namespace Chess.Services;

public class MoveService : IMoveService
{
    public List<Move> GenerateMoves(GridItem[,] grid, int row, int col)
    {
        var item = grid.GetItemAtPosition(row, col);

        var moves = new List<Move>();

        switch (item.CharacterCode)
        {
            case Constants.PawnDisplayCharacter:
                AddPawnMoves(grid, item, moves);
                break;

            case Constants.KingDisplayCharacter:
                AddKingMoves(grid, item, moves);
                break;

            case Constants.QueenDisplayCharacter:
                AddQueenMoves(grid, item, moves);
                break;

            case Constants.BishopDisplayCharacter:
                AddBishopMoves(grid, item, moves);
                break;

            case Constants.KnightDisplayCharacter:
                AddKnightMoves(grid, item, moves);
                break;

            case Constants.RookDisplayCharacter:
                AddRookMoves(grid, item, moves);
                break;
        }

        return moves;
    }

    private void AddCalculatedMove(GridItem[,] grid, GridItem item, List<Move> moves, sbyte deltaRow, sbyte deltaCol, int? maxSlide, bool allowAttack)
    {
        for (var i = 1; i <= (maxSlide ?? Grid.Size); i++)
        {
            var targetRow = item.Row + (deltaRow * i);
            var targetCol = item.Column + (deltaCol * i);

            if (grid.CheckValidPosition(targetRow, targetCol))
            {
                var target = grid.GetItemAtPosition(targetRow, targetCol);

                if (target.Player == item.Player)
                    break;

                var move = new Move
                {
                    FromRow = item.Row,
                    FromColumn = item.Column,
                    ToRow = targetRow,
                    ToColumn = targetCol,
                };

                if (target.Player == item.Player?.GetOtherPlayer())
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

    private void AddPawnMoves(GridItem[,] grid, GridItem item, List<Move> moves)
    {
        switch (item.Player)
        {
            case Player.Black:
                AddCalculatedMove(grid, item, moves, -1, 0, 1, false);
                break;

            case Player.White:
                AddCalculatedMove(grid, item, moves, 1, 0, 1, false);
                break;
        }
    }

    private void AddKingMoves(GridItem[,] grid, GridItem item, List<Move> moves)
    {
        AddCalculatedMove(grid, item, moves, 1, 1, 1, true);
        AddCalculatedMove(grid, item, moves, 1, 0, 1, true);
        AddCalculatedMove(grid, item, moves, 1, -1, 1, true);

        AddCalculatedMove(grid, item, moves, -1, 1, 1, true);
        AddCalculatedMove(grid, item, moves, -1, 0, 1, true);
        AddCalculatedMove(grid, item, moves, -1, -1, 1, true);

        AddCalculatedMove(grid, item, moves, 0, 1, 1, true);
        AddCalculatedMove(grid, item, moves, 0, -1, 1, true);
    }

    private void AddQueenMoves(GridItem[,] grid, GridItem item, List<Move> moves)
    {
        AddCalculatedMove(grid, item, moves, 1, 1, null, true);
        AddCalculatedMove(grid, item, moves, 1, 0, null, true);
        AddCalculatedMove(grid, item, moves, 1, -1, null, true);

        AddCalculatedMove(grid, item, moves, -1, 1, null, true);
        AddCalculatedMove(grid, item, moves, -1, 0, null, true);
        AddCalculatedMove(grid, item, moves, -1, -1, null, true);

        AddCalculatedMove(grid, item, moves, 0, 1, null, true);
        AddCalculatedMove(grid, item, moves, 0, -1, null, true);
    }

    private void AddBishopMoves(GridItem[,] grid, GridItem item, List<Move> moves)
    {
        AddCalculatedMove(grid, item, moves, 1, 1, null, true);
        AddCalculatedMove(grid, item, moves, 1, -1, null, true);
        AddCalculatedMove(grid, item, moves, -1, 1, null, true);
        AddCalculatedMove(grid, item, moves, -1, -1, null, true);
    }

    private void AddKnightMoves(GridItem[,] grid, GridItem item, List<Move> moves)
    {
        AddCalculatedMove(grid, item, moves, 1, 2, 1, true);
        AddCalculatedMove(grid, item, moves, 1, -2, 1, true);
        AddCalculatedMove(grid, item, moves, -1, 2, 1, true);
        AddCalculatedMove(grid, item, moves, -1, -2, 1, true);

        AddCalculatedMove(grid, item, moves, 2, 1, 1, true);
        AddCalculatedMove(grid, item, moves, 2, -1, 1, true);
        AddCalculatedMove(grid, item, moves, -2, 1, 1, true);
        AddCalculatedMove(grid, item, moves, -2, -1, 1, true);
    }

    private void AddRookMoves(GridItem[,] grid, GridItem item, List<Move> moves)
    {
        AddCalculatedMove(grid, item, moves, 1, 0, null, true);
        AddCalculatedMove(grid, item, moves, -1, 0, null, true);
        AddCalculatedMove(grid, item, moves, 0, 1, null, true);
        AddCalculatedMove(grid, item, moves, 0, -1, null, true);
    }
}
