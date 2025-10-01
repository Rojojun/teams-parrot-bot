using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EchoBot.Repository;
using Microsoft.Bot.Builder;

namespace EchoBot.Command;

public class ShowCommand(ICacheRepository cacheRepository) : ICommand
{
    public async Task ExecuteAsync(ITurnContext turnContext, CancellationToken cancellationToken)
    {
        var conversationId = turnContext.Activity.Conversation.Id;
        var items = cacheRepository.FindByKey(conversationId);

        var message = items.Any()
            ? "저장된 메시지 목록:\n- " + string.Join("\n- ", items)
            : "저장된 메시지가 없습니다.";

        await turnContext.SendActivityAsync(MessageFactory.Text(message), cancellationToken);
    }
}
