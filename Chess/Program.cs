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
            args = new string[] { "start-game" };
        }

        var client = Client.Create()
            .AddServices(services =>
            {
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

    public GameService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _displayService = _serviceProvider.GetRequiredService<IDisplayService>();
    }

    private void RunFullVisualMovesTest()
    {
        var grid = Grid.Create();

        for (var row = 0; row < Grid.Size; row++)
        {
            for (var col = 0; col < Grid.Size; col++)
            {
                if (grid[row, col].Piece is not null)
                {
                    _displayService.ApplyMoveHighlights(grid, row, col);
                    _displayService.Send(grid);
                }
            }
        }

        for (var col = 0; col < Grid.Size; col++)
        {
            Grid.ForceSwap(grid, 1, col, 2, col);
            Grid.ForceSwap(grid, 6, col, 5, col);
        }

        for (var row = 0; row < Grid.Size; row++)
        {
            for (var col = 0; col < Grid.Size; col++)
            {
                if (grid[row, col].Piece is not null)
                {
                    _displayService.ApplyMoveHighlights(grid, row, col);
                    _displayService.Send(grid);
                }
            }
        }
    }

    private void RunVisualMovesTest()
    {
        var grid = Grid.Create();

        _displayService.ApplyMoveHighlights(grid, 1, 1);
        _displayService.Send(grid);

        Grid.ForceSwap(grid, 6, 1, 2, 2);
        Grid.ForceSwap(grid, 6, 3, 2, 4);

        _displayService.ApplyMoveHighlights(grid, 1, 3);
        _displayService.Send(grid);

        _displayService.ApplyMoveHighlights(grid, 1, 2);
        _displayService.Send(grid);

        _displayService.ApplyMoveHighlights(grid, 7, 1);
        _displayService.Send(grid);

        Grid.ForceSwap(grid, 7, 5, 4, 4);
        _displayService.ApplyMoveHighlights(grid, 4, 4);
        _displayService.Send(grid);
        Grid.ForceSwap(grid, 4, 4, 7, 5);

        _displayService.ApplyMoveHighlights(grid, 7, 2);
        _displayService.Send(grid);

        Grid.ForceSwap(grid, 7, 7, 4, 0);
        _displayService.ApplyMoveHighlights(grid, 4, 0);
        _displayService.Send(grid);
    }

    [Surface("start-game")]
    public void StartGame()
    {
        //RunFullVisualMovesTest();
        //RunVisualMovesTest();

        var grid = Grid.Create();

        _displayService.Send(grid);

        var playerOne = _serviceProvider.GetRequiredService<IPlayerOne>() as IPlayer;
        var playerTwo = _serviceProvider.GetRequiredService<IPlayerTwo>() as IPlayer;

        while (true)
        {
            var item = playerOne.GetPieceSelection(grid);
            break;
        }
    }

    public void Start()
    {

    }
}
