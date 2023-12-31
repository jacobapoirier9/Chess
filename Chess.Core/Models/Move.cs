﻿namespace Chess.Core.Models;

public class Move
{
    public override string ToString() => $"({From.Row}, {From.Column}) -> ({To.Column}, {To.Column})";

    public Point From { get; set; }

    public Point To { get; set; }

    public bool IsAttack { get; set; }

    public bool IsCastling { get; set; }
}
