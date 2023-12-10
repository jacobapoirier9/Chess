using Chess.Core;
using Chess.Core.Models;
using Chess.Core.Services;
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

                RegisterOriginalGameInputs(services);

                services.AddSingleton<IMoveService, MoveService>();
                services.AddSingleton<IDisplayService, ConsoleDisplayService>();

                services.AddSingleton<GameService>();
            });

        await client.RunAsync(args);
    }

    private static void RegisterCastlingRightsGameInputs(IServiceCollection services)
    {
        // WHITE
        services.AddSingleton<IPlayerOne>(new MemoryInputPlayer(
            "b2", "b3",
            "c1", "a3",
            "b1", "c3",
            "d1", "a1"
        ));

        // BLACK
        services.AddSingleton<IPlayerTwo>(new MemoryInputPlayer(
            "b7", "b6",
            "c8", "a6",
            "b8", "c6",
            "d8", "a8"
        ));
    }

    private static void RegisterOriginalGameInputs(IServiceCollection services)
    {
        // WHITE
        services.AddSingleton<IPlayerOne>(new MemoryInputPlayer(
            "e2", "e3",
            "b1", "c3",
            "b2", "b4",
            "c1", "a3",
            "d1", "a1",
            "f1", "e2",
            "g1", "h3",
            "e1", "f1",
            "f1", "e1",
            "e1", "a8"
        ));

        // BLACK
        services.AddSingleton<IPlayerTwo>(new MemoryInputPlayer(
            "h7", "h6",
            "h6", "h5",
            "g7", "g6",
            "g6", "g5",
            "g5", "g4",
            "g4", "g3",
            "a7", "a6",
            "a6", "a5",
            "a5", "a4"
        ));
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

    private void DisplayServiceQuickDraw(FenObject fen, string input)
    {
        var point = PointMapping.ToPoint(input);
        DisplayServiceQuickDraw(fen, point);
    }

    private void RunManulOverrideGame()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Qk e3 3 38";

        var fen = _fenStringService.ParseFenString(input);

        var fenString = (_fenStringService as FenStringService).GenerateGridSegment(fen);

        DisplayServiceQuickDraw(fen, new Point(1, 1));

        Helper.ForceSwap(fen.Grid, new Point(6, 1), new Point(2, 2));
        Helper.ForceSwap(fen.Grid, new Point(6, 3), new Point(2, 4));

        DisplayServiceQuickDraw(fen, new Point(1, 3));

        DisplayServiceQuickDraw(fen, new Point(1, 2));

        DisplayServiceQuickDraw(fen, new Point(7, 1));

        Helper.ForceSwap(fen.Grid, new Point(7, 5), new Point(4, 4));
        DisplayServiceQuickDraw(fen, new Point(4, 4));
        Helper.ForceSwap(fen.Grid, new Point(4, 4), new Point(7, 5));

        DisplayServiceQuickDraw(fen, new Point(7, 2));

        Helper.ForceSwap(fen.Grid, new Point(7, 7), new Point(4, 0));
        DisplayServiceQuickDraw(fen, new Point(4, 0));

        Helper.ForceSwap(fen.Grid, new Point(1, 7), new Point(2, 7));
        DisplayServiceQuickDraw(fen, new Point(7, 2));

        var test = (_fenStringService as FenStringService).GenerateGridSegment(fen);
        fen.Grid = _fenStringService.ParseGridSegment(test);

        DisplayServiceQuickDraw(fen, new Point(7, 2));
    }

    [Surface("start-test")]
    public void StartTest()
    {
        var fen = _fenStringService.ParseFenString("r2q4/8/8/8/8/8/8/8 b QKqk - 8 5");
        _displayService.Draw(fen);

        DisplayServiceQuickDraw(fen, "d8");


        GameLoop();
        return;
    }

    private void GameLoop()
    {
        var fen = _fenStringService.ParseFenString(Constants.StartingFenString);

        while (true)
        {
            try
            {
                _displayService.Draw(fen);

                Console.WriteLine("{0} is up", fen.ActivePlayer);

                IPlayer currentPlayerService = fen.ActivePlayer switch
                {
                    Player.White => _playerOne,
                    Player.Black => _playerTwo,

                    _ => throw new IndexOutOfRangeException()
                };

                var selection = currentPlayerService.GetPieceSelectionPoint(fen);
                var moves = _moveService.GenerateMoves(fen, selection);
                _displayService.Draw(fen, selection, moves);

                var target = currentPlayerService.GetPieceMovementSelectionPoint(fen);
                _moveService.ExecuteMove(fen, selection, target, moves);

                var fenString = _fenStringService.GenerateFenString(fen);
                fen = _fenStringService.ParseFenString(fenString);

                Console.WriteLine(fenString);
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
