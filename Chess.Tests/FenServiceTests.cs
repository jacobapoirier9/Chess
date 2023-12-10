using Chess.Core;
using Chess.Core.Models;
using Chess.Core.Services;

namespace Chess.Tests;

public class FenServiceTests
{
    // The official starting FEN string looks like this:
    // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1

    private readonly IFenStringService _fenStringService = new FenStringService();

    [Theory]
    [InlineData(Constants.StartingFenString)]
    public void FenString_FullFenString_InputOutputMatch(string input)
    {
        var parsed = _fenStringService.ParseFenString(input);
        var output = _fenStringService.GenerateFenString(parsed);
        Assert.Equal(input, output);
    }

    // http://bernd.bplaced.net/fengenerator/fengenerator.html
    [Theory]
    [InlineData("8/8/8/8/K7/4k3/8/8")]
    [InlineData("8/8/Q1P5/4P2n/1R6/b3k1PK/8/4n3")]
    [InlineData("RN2rn2/8/3kq3/5r1p/1P1K2pb/1pPB4/p7/8")]
    [InlineData("n5K1/2Q4P/4P3/p4P1p/k6P/2r2b1p/3nPppp/q4N2")]
    [InlineData("N7/PP1BR1Nb/Q1Pr3P/3qn2p/pnPrPpp1/B1ppk1P1/2R1Pppb/1K6")]
    [InlineData("bN6/2p2P1P/2pQ1P1r/kn1qPNPr/2p1pp1P/b2p1P1P/p1nR1K1R/2B5")]
    [InlineData("r1bqk1nr/ppp1p1p1/2P1Q2N/1P1BN1pK/n1Rpbp2/2R3P1/PPPP3P/5B2")]
    public void FenString_PiecePlacement_InputOutputMatch(string input)
    {
        var grid = _fenStringService.ParseGridSegment(input);
        var output = _fenStringService.GenerateGridSegment(new FenObject { Grid = grid });

        Assert.Equal(input, output);
    }

    [Theory]
    [InlineData("w", Player.White)]
    [InlineData("b", Player.Black)]
    public void FenString_ActivePlayer_InputOutputMatch(string input, Player player)
    {
        var parsed = _fenStringService.ParseActivePlayerSegment(input);
        Assert.Equal(player, parsed);

        var output = _fenStringService.GenerateActivePlayerSegment(new FenObject { ActivePlayer = parsed });
        Assert.Equal(input, output);
    }

    [Theory]
    [InlineData("KQkq", true, true, true, true)]
    [InlineData("KQk", true, true, true, false)]
    [InlineData("KQq", true, true, false, true)]
    [InlineData("KQ", true, true, false, false)]
    [InlineData("Kkq", true, false, true, true)]
    [InlineData("Kk", true, false, true, false)]
    [InlineData("Kq", true, false, false, true)]
    [InlineData("K", true, false, false, false)]
    [InlineData("Qkq", false, true, true, true)]
    [InlineData("Qk", false, true, true, false)]
    [InlineData("Qq", false, true, false, true)]
    [InlineData("Q", false, true, false, false)]
    [InlineData("kq", false, false, true, true)]
    [InlineData("k", false, false, true, false)]
    [InlineData("q", false, false, false, true)]
    [InlineData("-", false, false, false, false)]
    public void FenString_CastlingRights_InputOutputMatch(string input, bool whiteKingSide, bool whiteQueenSide, bool blackkingSide, bool blackQueenSide)
    {
        var parsed = _fenStringService.ParseCasltingRightsSegment(input);

        if (whiteKingSide) Assert.True(parsed.WhiteKingSide); else Assert.False(parsed.WhiteKingSide);
        if (whiteQueenSide) Assert.True(parsed.WhiteQueenSide); else Assert.False(parsed.WhiteQueenSide);
        if (blackkingSide) Assert.True(parsed.BlackKingSide); else Assert.False(parsed.BlackKingSide);
        if (blackQueenSide) Assert.True(parsed.BlackQueenSide); else Assert.False(parsed.BlackQueenSide);

        var output = _fenStringService.GenerateCastlingRightsSegment(new FenObject { CastlingRights = parsed });
        Assert.Equal(input, output);
    }

    [Theory]
    [InlineData("a3", 5, 0)]
    [InlineData("b3", 5, 1)]
    [InlineData("c3", 5, 2)]
    [InlineData("d3", 5, 3)]
    [InlineData("e3", 5, 4)]
    [InlineData("f3", 5, 5)]
    [InlineData("g3", 5, 6)]
    [InlineData("h3", 5, 7)]
    [InlineData("a6", 2, 0)]
    [InlineData("b6", 2, 1)]
    [InlineData("c6", 2, 2)]
    [InlineData("d6", 2, 3)]
    [InlineData("e6", 2, 4)]
    [InlineData("f6", 2, 5)]
    [InlineData("g6", 2, 6)]
    [InlineData("h6", 2, 7)]
    [InlineData("-", null, null)]
    public void FenString_PossibleEnPassantTarget_InputOutputMatch(string input, int? row, int? column)
    {
        var parsed = _fenStringService.ParsePossibleEnPassantSegment(input);

        if (row.HasValue && column.HasValue)
        {
            Assert.NotNull(parsed);
            Assert.Equal(new Point(row.Value, column.Value), parsed);
        }
        else
        {
            Assert.Null(parsed);
        }

        var output = _fenStringService.GeneratePossibleEnPassantSegment(new FenObject { PossibleEnPassantTarget = parsed });
        Assert.Equal(input, output);
    }

    [Theory]
    [InlineData("5", 5)]
    public void FenString_HalfMoveClock_InputOutputMatch(string input, int halfMoveClock)
    {
        var parsed = _fenStringService.ParseHalfClockSegment(input);
        Assert.Equal(halfMoveClock, parsed);

        var output = _fenStringService.GenerateHalfClockSegment(new FenObject { HalfMoveClock = parsed });
        Assert.Equal(input, output);
    }

    [Theory]
    [InlineData("5", 5)]
    public void FenString_FullMoveNumber_InputOutputMatch(string input, int halfMoveClock)
    {
        var parsed = _fenStringService.ParseFullMoveNumberSegment(input);
        Assert.Equal(halfMoveClock, parsed);

        var output = _fenStringService.GenerateFullMoveNumberSegment(new FenObject { FullMoveNumber = parsed });
        Assert.Equal(input, output);
    }
}