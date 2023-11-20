namespace Chess.Services;

public class FenStringService : IFenStringService
{
    private const char _segmentSeparator = ' ';
    private const char _piecePlacementRowSeparator = '/';
    private const char _emptyField = '-';

    public FenObject Parse(string fen)
    {
        var segments = fen.Split(_segmentSeparator);

        return new FenObject
        {
            Grid = ParsePiecePlacement(segments[0]),
            ActivePlayer = ParseActiveColor(segments[1]),
            CastlingRights = ParseCastlingRights(segments[2])
        };
    }

    public GridItem[,] ParsePiecePlacement(string piecePlacementSegment)
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

    public Player ParseActiveColor(string activeColorSegment)
    {
        var player = Convert.ToChar(activeColorSegment) switch
        {
            'w' => Player.White,
            'b' => Player.Black,

            _ => throw new IndexOutOfRangeException()
        };

        return player;
    }

    public List<CastlingRight> ParseCastlingRights(string castlingRightsSegment)
    {
        if (castlingRightsSegment == _emptyField.ToString())
            return null;

        var castlingRights = castlingRightsSegment.Select(next => new CastlingRight
        {
            CharacterCode = char.ToUpper(next),
            Player = char.IsLower(next) ? Player.Black : Player.White
        }).ToList();

        return castlingRights;
    }
}
