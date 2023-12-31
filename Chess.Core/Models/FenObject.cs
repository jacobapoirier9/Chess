﻿namespace Chess.Core.Models;

public class FenObject
{
    public char?[,] Grid { get; set; }

    public Player ActivePlayer { get; set; }

    public CastlingRights CastlingRights { get; set; }

    public Point? PossibleEnPassantTarget { get; set; }

    public int HalfMoveClock { get; set; }

    public int FullMoveNumber { get; set; }
}