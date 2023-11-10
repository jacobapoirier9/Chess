using Chess.Enums;
using Chess.Model;
using System.Diagnostics;

namespace Chess.Model.Pieces;

public abstract class Piece
{
    public Player Player { get; private init; }
    public char CharacterCode { get; private init; }

    public List<TemplateMove> AvailableMoves { get; protected init; }

    public Piece(Player player, char characterCode)
    {
        Player = player;
        CharacterCode = characterCode;
    }
}
