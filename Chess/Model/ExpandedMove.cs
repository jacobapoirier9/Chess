using Chess.Enums;
using Chess.Model.Pieces;

namespace Chess.Model;

public class ExpandedMove
{
    public required TemplateMove Template { get; set; }

    public required int FromRow { get; set; }

    public required int FromCol { get; set; }

    public required int TargetRow { get; set; }

    public required int TargetCol { get; set; }

    public required MoveType MoveType { get; set; }

    public Piece TargetPiece { get; set; }
}