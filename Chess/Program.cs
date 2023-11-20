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
        var input = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b";

        var fen = _fenStringService.Parse(input);

        var grid = fen.Grid;

        _displayService.Send(grid, 1, 1);

        Grid.ForceSwap(grid, 6, 1, 2, 2);
        Grid.ForceSwap(grid, 6, 3, 2, 4);

        _displayService.Send(grid, 1, 3);

        _displayService.Send(grid, 1, 2);

        _displayService.Send(grid, 7, 1);

        Grid.ForceSwap(grid, 7, 5, 4, 4);
        _displayService.Send(grid, 4, 4);
        Grid.ForceSwap(grid, 4, 4, 7, 5);

        _displayService.Send(grid, 7, 2);

        Grid.ForceSwap(grid, 7, 7, 4, 0);
        _displayService.Send(grid, 4, 0);

        Grid.ForceSwap(grid, 1, 7, 2, 7);
        _displayService.Send(grid, 7, 2);
    }

    public void Start()
    {

    }
}
