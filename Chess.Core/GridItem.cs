﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Core;

public class GridItem
{
    public override string ToString() => $"{Player} {CharacterCode}";

    public char? CharacterCode { get; set; }

    public Player? Player { get; set; }
}