namespace Chess.Core;

public struct CastlingRight
{
    public override string ToString() => $"{Player} {CharacterCode}";

    public Player Player { get; set; }

    public char CharacterCode { get; set; }
}