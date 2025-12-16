using PureFix.Types;


namespace PureFix.Examples.Shared;

public class ConsoleLogFactory : ILogFactory
{
    private readonly IFixClock _clock;

    public ConsoleLogFactory(IFixClock clock)
    {
        _clock = clock;
    }

    public ILogger MakeLogger(string name)
    {
        return new ConsoleLogger(name, _clock);
    }

    public ILogger MakePlainLogger(string name)
    {
        return new ConsoleLogger(name, _clock);
    }
}
