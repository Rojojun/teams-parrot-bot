using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace EchoBot.Command;

public interface ICommand
{
    Task ExecuteAsync(ITurnContext turnContext, CancellationToken cancellationToken);
}
