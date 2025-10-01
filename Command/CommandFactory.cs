using EchoBot.Repository;

namespace EchoBot.Command;

public class CommandFactory(ICacheRepository cacheRepository)
{
    public ICommand CreateCommand(string input)
    {
        return input?.ToLower() switch
        {
            "bot help" or "bot -help" or "bot -?" => new HelpCommand(),
            "bot show" => new ShowCommand(cacheRepository),
            _ => new StoreCommand(cacheRepository)
        };
    }
}
