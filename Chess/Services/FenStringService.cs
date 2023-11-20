using System.ComponentModel;

namespace Chess.Services;

public class FenStringService : IFenStringService
{
    private const char _segmentSeparator = ' ';
    private const char _piecePlacementRowSeparator = '/';
    private const char _emptyField = '-';

    private string GetSegmentAndValidateExists(IEnumerable<string> items, int index, string missingSegment)
    {
        var item = items.ElementAtOrDefault(index);
        if (item is null)
            throw new FormatException($"An error occurred while parsing the FEN string. Invalid {missingSegment}");

        return item;
    }

    public FenObject Parse(string fen)
    {
        var segments = fen.Split(_segmentSeparator);

        return new FenObject
        {
            Grid = ParsePiecePlacement(GetSegmentAndValidateExists(segments, 0, nameof(FenObject.Grid))),
            ActivePlayer = ParseActiveColor(GetSegmentAndValidateExists(segments, 1, nameof(FenObject.ActivePlayer))),
            CastlingRights = ParseCastlingRights(GetSegmentAndValidateExists(segments, 2, nameof(FenObject.CastlingRights))),
            PossibleEnPassantTarget = ParseEnPassantTarget(GetSegmentAndValidateExists(segments, 3, nameof(FenObject.PossibleEnPassantTarget))),
            HalfMoveClock = ParseHalfMoveClock(GetSegmentAndValidateExists(segments, 4, nameof(FenObject.HalfMoveClock))),
            FullMoveNumber = ParseFullMoveNumber(GetSegmentAndValidateExists(segments, 5, nameof(FenObject.FullMoveNumber))),
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

    public Point? ParseEnPassantTarget(string possibleEnPassantTargetsSegment)
    {
        if (possibleEnPassantTargetsSegment == _emptyField.ToString())
            return null;

        var coords = ParseLetterNumberToNumberNumber(possibleEnPassantTargetsSegment);
        return coords;
    }

    public int ParseHalfMoveClock(string halfmoveClockSegment)
    {
        var value = int.Parse(halfmoveClockSegment);
        return value;
    }

    public int ParseFullMoveNumber(string fullmoveNumberSegment)
    {
        var value = int.Parse(fullmoveNumberSegment);
        return value;
    }

    // TODO: There's got to be a better way of handling this..
    /// <summary>
    /// Convert FEN string friendly coordinate to something the chess engine can use.
    /// </summary>
    private Point ParseLetterNumberToNumberNumber(string letterNumber)
    {
        var columnLetter = Convert.ToChar(letterNumber[0]);
        var rowLetter = Convert.ToChar(letterNumber[1]);

        var columnNumber = columnLetter switch
        {
            'a' => 0,
            'b' => 1,
            'c' => 2,
            'd' => 3,
            'e' => 4,
            'f' => 5,
            'g' => 6,
            'h' => 7,

            _ => throw new IndexOutOfRangeException()
        };

        var rowNumber = rowLetter switch
        {
            '8' => 0,
            '7' => 1,
            '6' => 2,
            '5' => 3,
            '4' => 4,
            '3' => 5,
            '2' => 6,
            '1' => 7,

            _ => throw new IndexOutOfRangeException()
        };

        return new Point(rowNumber, columnNumber);
    }
}
