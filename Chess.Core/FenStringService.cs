namespace Chess.Core;

public class FenStringService : IFenStringService
{
    private string GetFenSegment(IEnumerable<string> items, int index, string missingSegment)
    {
        var item = items.ElementAtOrDefault(index);
        if (item is null)
            throw new FormatException($"An error occurred while parsing the FEN string. Invalid {missingSegment}");

        return item;
    }

    public FenObject ParseFenString(string fen)
    {
        var segments = fen.Split(Constants.FenStringSegmentSeparator);

        return new FenObject
        {
            Grid = ParseGridSegment(GetFenSegment(segments, 0, nameof(FenObject.Grid))),
            ActivePlayer = ParseActivePlayerSegment(GetFenSegment(segments, 1, nameof(FenObject.ActivePlayer))),
            CastlingRights = ParseCasltingRightsSegment(GetFenSegment(segments, 2, nameof(FenObject.CastlingRights))),
            PossibleEnPassantTarget = ParsePossibleEnPassantSegment(GetFenSegment(segments, 3, nameof(FenObject.PossibleEnPassantTarget))),
            HalfMoveClock = ParseHalfClockSegment(GetFenSegment(segments, 4, nameof(FenObject.HalfMoveClock))),
            FullMoveNumber = ParseFullMoveNumberSegment(GetFenSegment(segments, 5, nameof(FenObject.FullMoveNumber))),
        };
    }

    public GridItem[,] ParseGridSegment(string segment)
    {
        var grid = new GridItem[Constants.GridSize, Constants.GridSize];

        var lines = segment.Split(Constants.FenStringPiecePlacementLineSeparator);
        for (var row = 0; row < lines.Length; row++)
        {
            var line = lines[row];

            var column = 0;
            var lineIndex = 0;

            while (column < Constants.GridSize)
            {
                var next = Convert.ToChar(line.ElementAt(lineIndex++));

                if (char.IsNumber(next))
                {
                    column += Convert.ToInt16(next.ToString());
                    continue;
                }

                grid[row, column] = new GridItem
                {
                    CharacterCode = char.ToUpper(next),
                    Player = char.IsLower(next) ? Player.Black : Player.White
                };

                column++;
            }
        }

        return grid;
    }

    public Player ParseActivePlayerSegment(string segment)
    {
        var player = Convert.ToChar(segment) switch
        {
            'w' => Player.White,
            'b' => Player.Black,

            _ => throw new IndexOutOfRangeException()
        };

        return player;
    }

    public List<CastlingRight> ParseCasltingRightsSegment(string segment)
    {
        if (segment == Constants.FenStringEmptyFieldCharacter.ToString())
            return null;

        var castlingRights = segment.Select(next => new CastlingRight
        {
            CharacterCode = char.ToUpper(next),
            Player = char.IsLower(next) ? Player.Black : Player.White
        }).ToList();

        return castlingRights;
    }

    public Point? ParsePossibleEnPassantSegment(string segment)
    {
        if (segment == Constants.FenStringEmptyFieldCharacter.ToString())
            return null;

        var point = FriendlyToPoint(segment);
        return point;
    }

    public int ParseHalfClockSegment(string segment)
    {
        var value = int.Parse(segment);
        return value;
    }

    public int ParseFullMoveNumberSegment(string segment)
    {
        var value = int.Parse(segment);
        return value;
    }

    // TODO: There's got to be a better way of handling this..
    /// <summary>
    /// Convert FEN string friendly coordinate to something the chess engine can use.
    /// </summary>
    private Point FriendlyToPoint(string letterNumber)
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

    public string GenerateFenString(FenObject fen)
    {
        var fenString = string.Join(Constants.FenStringSegmentSeparator, new string[]
        {
            GenerateGridSegment(fen.Grid),
            GenerateActivePlayerSegment(fen.ActivePlayer)

            //Grid = ParseGridSegment(GetFenSegment(segments, 0, nameof(FenObject.Grid))),
            //ActivePlayer = ParseActivePlayerSegment(GetFenSegment(segments, 1, nameof(FenObject.ActivePlayer))),
            //CastlingRights = ParseCasltingRightsSegment(GetFenSegment(segments, 2, nameof(FenObject.CastlingRights))),
            //PossibleEnPassantTarget = ParsePossibleEnPassantSegment(GetFenSegment(segments, 3, nameof(FenObject.PossibleEnPassantTarget))),
            //HalfMoveClock = ParseHalfClockSegment(GetFenSegment(segments, 4, nameof(FenObject.HalfMoveClock))),
            //FullMoveNumber = ParseFullMoveNumberSegment(GetFenSegment(segments, 5, nameof(FenObject.FullMoveNumber))),
        });

        return fenString;
    }

    public string GenerateGridSegment(GridItem[,] grid)
    {
        var lines = new List<string>();
        for (var row = 0; row < grid.GetLength(0); row++)
        {
            var line = string.Empty;

            for (var column = 0; column < grid.GetLength(1); column++)
            {
                var item = grid.GetItemAtPositionOrDefault(new Point(row, column));

                if (item is null || item.CharacterCode is null || item.Player is null)
                {
                    if (line.Length > 0)
                    {
                        var next = line.Last();
                        if (char.IsDigit(next))
                        {
                            var number = int.Parse(next.ToString());
                            line = line.TrimEnd(Convert.ToChar(number.ToString()));

                            number++;
                            line += number.ToString();
                        }
                        else
                        {
                            line += "1";
                        }
                    }
                    else
                    {
                        line += "1";
                    }
                }
                else
                {
                    var next = item.Player switch
                    {
                        Player.Black => char.ToLower(item.CharacterCode.Value),
                        Player.White => item.CharacterCode.Value,

                        _ => throw new IndexOutOfRangeException()
                    };

                    line += next;
                }
            }

            lines.Add(line);
        }

        return string.Join(Constants.FenStringPiecePlacementLineSeparator, lines);
    }

    public string GenerateActivePlayerSegment(Player player)
    {
        var activeColorSegment = player switch
        {
            Player.Black => "b",
            Player.White => "w",

            _ => throw new IndexOutOfRangeException()
        };

        return activeColorSegment;
    }
}
