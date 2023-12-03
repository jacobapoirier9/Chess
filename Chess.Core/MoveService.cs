﻿namespace Chess.Core;

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

    private bool CanCastleWhiteQueenSide(FenObject fen)
    {
        return fen.ActivePlayer == Player.White
            && fen.CastlingRights.WhiteQueenSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.WhiteBaseLineRow, Constants.LeftKnightStartingColumn)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.WhiteBaseLineRow, Constants.LeftBishopStartingColumn)) is null;
    }

    private bool CanCastleWhiteKingSide(FenObject fen)
    {
        return fen.ActivePlayer == Player.White
            && fen.CastlingRights.WhiteKingSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.WhiteBaseLineRow, Constants.RightBishopStartingColumn)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.WhiteBaseLineRow, Constants.RightKnightStartingColumn)) is null;
    }

    private bool CanCastleBlackQueenSide(FenObject fen)
    {
        return fen.ActivePlayer == Player.Black
            && fen.CastlingRights.BlackQueenSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.BlackBaseLineRow, Constants.LeftKnightStartingColumn)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.BlackBaseLineRow, Constants.LeftBishopStartingColumn)) is null;
    }

    private bool CanCastleBlackKingSide(FenObject fen)
    {
        return fen.ActivePlayer == Player.Black
            && fen.CastlingRights.BlackKingSide
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.BlackBaseLineRow, Constants.RightBishopStartingColumn)) is null
            && fen.Grid.GetItemAtPositionOrDefault(new Point(Constants.BlackBaseLineRow, Constants.RightKnightStartingColumn)) is null;
    }

    private void AddCasltingMoves(FenObject fen, GridItem item, Point point, List<Move> moves)
    {
        var move = new Move
        {
            From = point,
            IsCastling = true
        };

        if (CanCastleWhiteQueenSide(fen))
        {
            if (item.CharacterCode == Constants.QueenDisplayCharacter)
            {
                move.To = new Point(Constants.WhiteBaseLineRow, Constants.LeftRookStartingColumn);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(Constants.WhiteBaseLineRow, Constants.LeftQueenStartingColumn);
            }
            else
            {
                return;
            }
        }

        else if (CanCastleWhiteKingSide(fen))
        {
            if (item.CharacterCode == Constants.KingDisplayCharacter)
            {
                move.To = new Point(Constants.WhiteBaseLineRow, Constants.RightRookStartingColumn);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(Constants.WhiteBaseLineRow, Constants.RightKingStartingColumn);
            }
            else
            {
                return;
            }
        }

        else if (CanCastleBlackQueenSide(fen))
        {
            if (item.CharacterCode == Constants.QueenDisplayCharacter)
            {
                move.To = new Point(Constants.BlackBaseLineRow, Constants.LeftRookStartingColumn);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(Constants.BlackBaseLineRow, Constants.LeftQueenStartingColumn);
            }
            else
            {
                return;
            }
        }

        else if (CanCastleBlackKingSide(fen))
        {
            if (item.CharacterCode == Constants.KingDisplayCharacter)
            {
                move.To = new Point(Constants.BlackBaseLineRow, Constants.RightRookStartingColumn);
            }
            else if (item.CharacterCode == Constants.RookDisplayCharacter)
            {
                move.To = new Point(Constants.BlackBaseLineRow, Constants.RightRookStartingColumn);
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


        var move = moves.Single(move => from == move.From && to == move.To); // TODO: Should this be moved to the caller?

        ApplyCastlingRightsRemoval(fen, move);
        ApplyPossibleEnPassantTarget(fen, move);
        ApplyHalfMoveClockTick(fen, move);
        ApplyFullMoveNumberTick(fen);

        ApplyPlayerSwitch(fen);
    }

    private void ApplyPossibleEnPassantTarget(FenObject fen, Move move)
    {
        if (fen.Grid.GetItemAtPositionOrDefault(move.From)?.CharacterCode == Constants.PawnDisplayCharacter)
        {
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

    private void ApplyCastlingRightsRemoval(FenObject fen, Move move)
    {
        if (fen.ActivePlayer == Player.Black && move.From.Row == Constants.BlackBaseLineRow)
        {
            if (move.From.Column == Constants.LeftRookStartingColumn || move.From.Column == Constants.LeftQueenStartingColumn)
            {
                fen.CastlingRights.BlackQueenSide = false;
            }
            else if (move.From.Column == Constants.RightRookStartingColumn || move.From.Column == Constants.RightKingStartingColumn)
            {
                fen.CastlingRights.BlackKingSide = false;
            }
        }
        else if (fen.ActivePlayer == Player.White && move.From.Row == Constants.WhiteBaseLineRow)
        {
            if (move.From.Column == Constants.LeftRookStartingColumn || move.From.Column == Constants.LeftQueenStartingColumn)
            {
                fen.CastlingRights.WhiteQueenSide = false;
            }
            else if (move.From.Column == Constants.RightRookStartingColumn || move.From.Column == Constants.RightKingStartingColumn)
            {
                fen.CastlingRights.WhiteKingSide = false;
            }
        }
    }

    private void ApplyHalfMoveClockTick(FenObject fen, Move move)
    {
        if (move.IsAttack || fen.Grid.GetItemAtPositionOrDefault(move.From)?.CharacterCode == Constants.PawnDisplayCharacter)
        {
            fen.HalfMoveClock = 1;
        }
        else
        {
            fen.HalfMoveClock++;
        }
    }

    private void ApplyFullMoveNumberTick(FenObject fen)
    {
        if (fen.ActivePlayer == Player.Black)
        {
            fen.FullMoveNumber++;
        }
    }

    private void ApplyPlayerSwitch(FenObject fen)
    {
        fen.ActivePlayer = fen.ActivePlayer.GetOtherPlayer();
    }
}
