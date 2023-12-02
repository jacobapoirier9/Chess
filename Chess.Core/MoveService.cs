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
                var blackSlide = point.Row == Constants.BlackPawnRow ? 2 : 1;
                AddCalculatedMove(fen, item, point, moves, 1, 0, blackSlide, false);
                break;

            case Player.White:
                var whiteSlide = point.Row == Constants.WhitePawnRow ? 2 : 1;
                AddCalculatedMove(fen, item, point, moves, -1, 0, whiteSlide, false);
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

        AddCasltingMoves(fen, item, point, moves);
    }

    private void AddQueenMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        AddCalculatedMove(fen, item, point, moves, 1, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, 1, 0, null, true);
        AddCalculatedMove(fen, item, point, moves, 1, -1, null, true);

        AddCalculatedMove(fen, item, point, moves, -1, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, -1, 0, null, true);
        AddCalculatedMove(fen, item, point, moves, -1, -1, null, true);

        AddCalculatedMove(fen, item, point, moves, 0, 1, null, true);
        AddCalculatedMove(fen, item, point, moves, 0, -1, null, true);

        AddCasltingMoves(fen, item, point, moves);
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
        AddCasltingMoves(fen, item, point, moves);
    }

    private void AddCasltingMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        var move = new Move
        {
            From = point,
            IsCastling = true
        };

        if (fen.ActivePlayer == Player.White 
            && fen.CastlingRights.WhiteQueenSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(7, 1)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(7, 2)) is null)
        {
            if (item.CharacterCode == Constants.QueenDisplayCharacter)
            {
                move.To = new Point(7, 0);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(7, 3);
            }
            else
            {
                return;
            }
        }

        else if (fen.ActivePlayer == Player.White
            && fen.CastlingRights.WhiteKingSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(7, 5)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(7, 6)) is null)
        {
            if (item.CharacterCode == Constants.KingDisplayCharacter)
            {
                move.To = new Point(7, 7);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(7, 4);
            }
            else
            {
                return;
            }
        }

        else if (fen.ActivePlayer == Player.Black
            && fen.CastlingRights.BlackQueenSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(0, 1)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(0, 2)) is null
            && item.CharacterCode == Constants.QueenDisplayCharacter)
        {
            if (item.CharacterCode == Constants.QueenDisplayCharacter)
            {
                move.To = new Point(0, 0);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(0, 3);
            }
            else
            {
                return;
            }
        }

        else if (fen.ActivePlayer == Player.Black
            && fen.CastlingRights.BlackKingSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(0, 5)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(0, 6)) is null
            && item.CharacterCode == Constants.KingDisplayCharacter)
        {
            if (item.CharacterCode == Constants.KingDisplayCharacter)
            {
                move.To = new Point(0, 7);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(0, 4);
            }
            else
            {
                return;
            }
        }

        else
        {
            return;
        }

        moves.Add(move);
    }

    public void ExecuteMove(FenObject fen, Point from, Point to, List<Move> moves)
    {
        // Just to make sure the executing move is present in the moves collection
        if (!moves.Exists(move => from == move.From && to == move.To))
        {
            throw new ApplicationException("Invalid move");
        }

        fen.Grid.ForceSwap(from, to);

        fen.ActivePlayer = fen.ActivePlayer.GetOtherPlayer();

        var move = moves.Single(move => from == move.From && to == move.To); // TODO: Should this be moved to the caller?
        ApplyPossibleEnPassantTarget(fen, move);
    }

    private void ApplyPossibleEnPassantTarget(FenObject fen, Move move)
    {
        if (fen.Grid.GetItemAtPositionOrDefault(move.From)?.CharacterCode != Constants.PawnDisplayCharacter)
        {
            return;
        }

        if (fen.ActivePlayer == Player.White
            && move.From.Row == Constants.WhitePawnRow
            && move.From.Column == Constants.WhitePawnRow - 2)
        {
            fen.PossibleEnPassantTarget = new Point(Constants.WhitePawnRow - 1, move.From.Column);
        }

        else if (fen.ActivePlayer == Player.Black
            && move.From.Row == Constants.BlackPawnRow
            && move.From.Column == Constants.BlackPawnRow + 2)
        {
            fen.PossibleEnPassantTarget = new Point(Constants.BlackPawnRow + 1, move.From.Column);
        }
    }
}
