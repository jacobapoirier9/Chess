using CommandSurfacer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chess;

public class ConsoleInputPlayer : IPlayer, IPlayerOne, IPlayerTwo
{
    public IGetInput GetInputs => _getInputs;

    private readonly IGetInput _getInputs;
    private readonly IStringConverter _stringConverter;

    public ConsoleInputPlayer(IServiceProvider serviceProvider)
    {
        _stringConverter = serviceProvider.GetRequiredService<IStringConverter>();
        _getInputs = new GetConsoleInput(_stringConverter);
    }
}
