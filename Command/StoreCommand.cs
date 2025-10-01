using System.Threading;
using System.Threading.Tasks;
using EchoBot.Repository;
using Microsoft.Bot.Builder;

namespace EchoBot.Command;

public class StoreCommand(ICacheRepository cacheRepository) : ICommand
{
    public async Task ExecuteAsync(ITurnContext turnContext, CancellationToken cancellationToken)
    {
        var text = turnContext.Activity.Text.Trim();
        var conversationId = turnContext.Activity.Conversation.Id;

        cacheRepository.Save(conversationId, text);

        await turnContext.SendActivityAsync(
            MessageFactory.Text($"'{text}' (이)가 저장되었습니다."),
            cancellationToken);
    }
}
