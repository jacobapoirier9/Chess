﻿namespace Chess.Services;

public class FenStringService : IFenStringService
{
    private const char _segmentSeparator = ' ';
    private const char _piecePlacementRowSeparator = '/';

    public void Parse(string fen)
    {
        var segments = fen.Split(_segmentSeparator);
        ParseGrid(segments[0]);
    }

    public GridItem[,] ParseGrid(string piecePlacementSegment)
    {
        var grid = new GridItem[Constants.GridSize, Constants.GridSize];

        var lines = piecePlacementSegment.Split(_piecePlacementRowSeparator);
        for (var row = 0; row < lines.Length; row++)
        {
            var line = lines[row];

            var column = 0;
            while (column < Constants.GridSize)
            {
                var next = Convert.ToChar(line.ElementAt(column));

                if (char.IsNumber(next))
                {
                    column += Convert.ToInt16(next.ToString());
                    continue;
                }

                grid[row, column] = new GridItem
                {
                    Row = row,
                    Column = column,

                    CharacterCode = char.ToUpper(next),
                    Player = char.IsLower(next) ? Player.Black : Player.White
                };

                column++;
            }
        }

        return grid;
    }
}