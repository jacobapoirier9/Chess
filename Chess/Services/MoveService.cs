using Chess.Enums;
using Chess.Model;
using Chess.Model.Pieces;
using static Chess.Services.MoveService;

namespace Chess.Services;

public class MoveService : IMoveService
{
    public struct Move
    {
        public int FromRow { get; set; }

        public int FromCol { get; set; }

        public int ToRow { get; set; }

        public int ToCol { get; set; }
    }

    public List<Move> GenerateMoves(GridItem[,] grid, int row, int col)
    {
        var item = grid.GetItemAtPosition(row, col);

        var moves = new List<Move>();

        switch (item.Piece.CharacterCode)
        {
            case Constants.QueenDisplayCharacter:
                AddDirectionalSlide(grid, item, moves, row, col, Direction.Positive, Direction.Positive);
                AddDirectionalSlide(grid, item, moves, row, col, Direction.Positive, Direction.Neutral);
                AddDirectionalSlide(grid, item, moves, row, col, Direction.Positive, Direction.Negative);

                AddDirectionalSlide(grid, item, moves, row, col, Direction.Negative, Direction.Positive);
                AddDirectionalSlide(grid, item, moves, row, col, Direction.Negative, Direction.Neutral);
                AddDirectionalSlide(grid, item, moves, row, col, Direction.Negative, Direction.Negative);

                AddDirectionalSlide(grid, item, moves, row, col, Direction.Neutral, Direction.Positive);
                AddDirectionalSlide(grid, item, moves, row, col, Direction.Neutral, Direction.Negative);
                break;
        }


        return moves;
    }

    // TODO: Finish coding this
    private void AddDirectionalSlide(GridItem[,] grid, GridItem item, List<Move> moves, int row, int col, Direction rowDirection, Direction colDirection, int maxSlide = Grid.Size)
    {
        for (var i = 1; i <= maxSlide; i++)
        {
            var hit = false;

            var targetRow = row + ((int)rowDirection * i);
            var targetCol = col + ((int)colDirection * i);

            if (grid.CheckValidPosition(targetRow, targetCol))
            {
                var target = grid.GetItemAtPosition(targetRow, targetCol);

                // Is the current expansion targeting the other player?
                if (item.Piece.Player.GetOtherPlayer() == target?.Piece?.Player)
                {
                    // TODO: Using this approach we allow the first attack to be added to the collection, but end up breaking out of the collection
                    if (hit)
                        break;

                    hit = true;
                }

                moves.Add(new Move
                {
                    FromRow = row,
                    FromCol = col,
                    ToRow = targetRow,
                    ToCol = targetCol,
                });
            }
            else
            {
                break;
            }
        }
    }



    public List<ExpandedMove> GetExpandedMoves(GridItem[,] grid, int row, int col)
    {
        var item = grid.GetItemAtPositionOrDefault(row, col);

        if (item?.Piece is null)
            throw new ApplicationException($"A valid piece was not found at {row}, {col}");

        var expanded = new List<ExpandedMove>();
        foreach (var move in item.Piece.AvailableMoves)
        {
            var expansionCount = move.AllowSlide && move.MaxSlideCount.HasValue ? move.MaxSlideCount.Value : 1;

            var hit = false;
            for (var i = 1; i <= expansionCount; i++)
            {
                var targetRow = row + move.MoveRows * (int)move.RowDirection * i;
                var targetCol = col + move.MoveCols * (int)move.ColDirection * i;

                if (grid.CheckValidPosition(targetRow, targetCol))
                {
                    var target = grid.GetItemAtPosition(targetRow, targetCol);

                    var targetMoveType = MoveType.PassiveOnly;

                    // Is the current expansion targeting the other player?
                    if (item.Piece.Player.GetOtherPlayer() == target?.Piece?.Player)
                    {
                        if (move.MoveType == MoveType.AttackOnly || move.MoveType == MoveType.PassiveOrAttack)
                        {
                            targetMoveType = MoveType.AttackOnly;

                            // TODO: Using this approach we allow the first attack to be added to the collection, but end up breaking out of the collection
                            if (hit)
                                break;

                            hit = true;
                        }
                        else
                        {
                            if (!move.AllowHopOver && target.Piece?.Player is not null)
                                break;

                            continue;
                        }
                    }
                    // Is the current expansion targeting the current player?
                    else if (item.Piece.Player == target?.Piece?.Player)
                    {
                        if (!move.AllowHopOver && target.Piece?.Player is not null)
                            break;

                        continue;
                    }
                    // Is the current expansion targeting no player?
                    else if (target?.Piece?.Player is null)
                    {
                        if (move.MoveType == MoveType.PassiveOnly || move.MoveType == MoveType.PassiveOrAttack)
                        {
                        }
                        else
                        {
                            continue;
                        }
                    }

                    expanded.Add(new ExpandedMove
                    {
                        Template = move,

                        FromRow = row,
                        FromCol = col,

                        TargetRow = targetRow,
                        TargetCol = targetCol,
                        MoveType = targetMoveType,

                        TargetPiece = target?.Piece
                    });
                }
            }
        }

        return expanded;
    }

    // GetExpandedMoves will not return a move that can not be executed. Should we even bother validating that we can in fact move there?
    public bool CanMove(GridItem[,] grid, ExpandedMove move)
    {
        // Require valid positions
        var validPositions = grid.CheckValidPosition(move.FromRow, move.FromCol) && grid.CheckValidPosition(move.TargetRow, move.TargetCol);
        if (!validPositions)
            return false;

        // Require from has a valid piece to move
        var from = grid.GetItemAtPosition(move.FromRow, move.FromCol);
        if (from.Piece is null)
            return false;

        // Require a double check on the target piece.
        var to = grid.GetItemAtPosition(move.TargetRow, move.TargetCol);
        return move.MoveType == MoveType.AttackOnly ?
            to.Piece is not null :
            to.Piece is null;
    }

    public void ExecuteMove(GridItem[,] grid, ExpandedMove move)
    {
        if (CanMove(grid, move))
        {
            if (move.MoveType == MoveType.AttackOnly)
                grid[move.TargetRow, move.TargetCol] = new GridItem();

            var from = grid.GetItemAtPosition(move.FromRow, move.FromCol);
            grid.ForceSwap(move.FromRow, move.FromCol, move.TargetRow, move.TargetCol);

            if (move.Template.RemoveAfterUse && from.Piece.AvailableMoves.Contains(move.Template))
            {
                var removed = from.Piece.AvailableMoves.Remove(move.Template);

            }
        }
        else
        {
            throw new ApplicationException();
        }
    }
}
