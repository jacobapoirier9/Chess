using Chess.Services;
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

                services.AddSingleton<IPlayerOne, ConsoleInputPlayer>();
                services.AddSingleton<IPlayerTwo, ConsoleInputPlayer>();
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

    public GameService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _displayService = _serviceProvider.GetRequiredService<IDisplayService>();
        _moveService = _serviceProvider.GetRequiredService<IMoveService>();
        _fenStringService = _serviceProvider.GetRequiredService<IFenStringService>();
    }

    [Surface("start-test")]
    public void StartTest()
    {
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Qk e3";

        var fen = _fenStringService.Parse(input);

        var grid = fen.Grid;

        _displayService.Send(grid, new Point(1, 1));

        Grid.ForceSwap(grid, new Point(6, 1), new Point(2, 2));
        Grid.ForceSwap(grid, new Point(6, 3), new Point(2, 4));

        _displayService.Send(grid, new Point(1, 3));

        _displayService.Send(grid, new Point(1, 2));

        _displayService.Send(grid, new Point(7, 1));

        Grid.ForceSwap(grid, new Point(7, 5), new Point(4, 4));
        _displayService.Send(grid, new Point(4, 4));
        Grid.ForceSwap(grid, new Point(4, 4), new Point(7, 5));

        _displayService.Send(grid, new Point(7, 2));

        Grid.ForceSwap(grid, new Point(7, 7), new Point(4, 0));
        _displayService.Send(grid, new Point(4, 0));

        Grid.ForceSwap(grid, new Point(1, 7), new Point(2, 7));
        _displayService.Send(grid, new Point(7, 2));
    }

    public void Start()
    {

    }
}
