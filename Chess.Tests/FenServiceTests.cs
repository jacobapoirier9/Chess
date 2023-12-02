using Chess.Core;

namespace Chess.Tests;

public class FenServiceTests
{
    private readonly IFenStringService _fenStringService = new FenStringService();

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
}