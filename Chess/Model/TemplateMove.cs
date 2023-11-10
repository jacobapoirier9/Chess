using Chess.Enums;

namespace Chess.Model;

public class TemplateMove
{
    public override int GetHashCode() => this.GenerateHashCode();

    public required MoveType MoveType { get; set; }

    public required int MoveRows { get; set; }

    public required Direction RowDirection { get; set; }

    public required int MoveCols { get; set; }

    public required Direction ColDirection { get; set; }

    public required bool AllowSlide { get; set; }

    public int? MaxSlideCount { get; set; }

    public required bool AllowHopOver { get; set; }

    public required bool RemoveAfterUse { get; set; }
}

