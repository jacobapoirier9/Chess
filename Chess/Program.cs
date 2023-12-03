using Chess.Core;
using CommandSurfacer;
using Microsoft.Extensions.DependencyInjection;

namespace Chess;

public static class Program
{
    public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();
    public static async Task MainAsync(string[] args)
    {
        if (System.Diagnostics.Debugger.IsAttached)
        {
            args = new string[] { "start-test" };
        }

        var client = Client.Create()
            .AddServices(services =>
            {
                services.AddSingleton<IFenStringService, FenStringService>();

                //services.AddSingleton<IPlayerOne, ConsoleInputPlayer>();
                //services.AddSingleton<IPlayerTwo, ConsoleInputPlayer>();

                // WHITE
                services.AddSingleton<IPlayerTwo>(new MemoryInputPlayer(
                    "64", "54",
                    "71", "52",
                    "61", "41",
                    "72", "50",
                    "73", "70",
                    "75", "64",
                    "76", "57",
                    "74", "75",
                    "75", "74",
                    "74", "00"
                ));

                // BLACK
                services.AddSingleton<IPlayerOne>(new MemoryInputPlayer(
                    "17", "27",
                    "27", "37",
                    "16", "26",
                    "26", "36",
                    "36", "46",
                    "46", "56",
                    "10", "20",
                    "20", "30",
                    "30", "40"
                ));

                //services.AddSingleton<IPlayerService, PlayerService>();

                services.AddSingleton<IMoveService, MoveService>();
                services.AddSingleton<IDisplayService, ConsoleDisplayService>();

                services.AddSingleton<GameService>();
            });

        await client.RunAsync(args);
    }
}

public class GameService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly IDisplayService _displayService;
    private readonly IMoveService _moveService;
    private readonly IFenStringService _fenStringService;

    private readonly IPlayerOne _playerOne;
    private readonly IPlayerTwo _playerTwo;

    public GameService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _displayService = _serviceProvider.GetRequiredService<IDisplayService>();
        _moveService = _serviceProvider.GetRequiredService<IMoveService>();
        _fenStringService = _serviceProvider.GetRequiredService<IFenStringService>();

        _playerOne = _serviceProvider.GetRequiredService<IPlayerOne>();
        _playerTwo = _serviceProvider.GetRequiredService<IPlayerTwo>();
    }

    private void DisplayServiceQuickDraw(FenObject fen, Point point)
    {
        var moves = _moveService.GenerateMoves(fen, point);
        _displayService.Draw(fen, point, moves);
    }

    [Surface("start-test")]
    public void StartTest()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Qk e3 3 38";

        var fen = _fenStringService.ParseFenString(input);

        var fenString = (_fenStringService as FenStringService).GenerateGridSegment(fen);

        DisplayServiceQuickDraw(fen, new Point(1, 1));

        Grid.ForceSwap(fen.Grid, new Point(6, 1), new Point(2, 2));
        Grid.ForceSwap(fen.Grid, new Point(6, 3), new Point(2, 4));

        DisplayServiceQuickDraw(fen, new Point(1, 3));

        DisplayServiceQuickDraw(fen, new Point(1, 2));

        DisplayServiceQuickDraw(fen, new Point(7, 1));

        Grid.ForceSwap(fen.Grid, new Point(7, 5), new Point(4, 4));
        DisplayServiceQuickDraw(fen, new Point(4, 4));
        Grid.ForceSwap(fen.Grid, new Point(4, 4), new Point(7, 5));

        DisplayServiceQuickDraw(fen, new Point(7, 2));

        Grid.ForceSwap(fen.Grid, new Point(7, 7), new Point(4, 0));
        DisplayServiceQuickDraw(fen, new Point(4, 0));

        Grid.ForceSwap(fen.Grid, new Point(1, 7), new Point(2, 7));
        DisplayServiceQuickDraw(fen, new Point(7, 2));

        var test = (_fenStringService as FenStringService).GenerateGridSegment(fen);
        fen.Grid = _fenStringService.ParseGridSegment(test);

        DisplayServiceQuickDraw(fen, new Point(7, 2));

        GameLoop();
        return;

    }

    private void GameLoop()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        var fen = _fenStringService.ParseFenString(input);

        while (true)
        {
            try
            {
                _displayService.Draw(fen);

                Console.WriteLine("{0} is up", fen.ActivePlayer);

                IPlayer currentPlayerService = fen.ActivePlayer switch
                {
                    Player.Black => _playerOne,
                    Player.White => _playerTwo,

                    _ => throw new IndexOutOfRangeException()
                };

                var selection = currentPlayerService.GetPieceSelectionPoint(fen);
                var moves = _moveService.GenerateMoves(fen, selection);
                _displayService.Draw(fen, selection, moves);

                var target = currentPlayerService.GetPieceMovementSelectionPoint(fen);
                _moveService.ExecuteMove(fen, selection, target, moves);
            }
            catch (Exception ex) // TODO: This is only here to provide an automatic break mechanism when the memory inputs run out of inputs
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Breaking game loop");
                break;
            }
        }
    }
}
