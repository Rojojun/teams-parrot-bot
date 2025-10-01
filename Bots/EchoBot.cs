// Generated with EchoBot .NET Template version v4.22.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EchoBot.Command;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot.Bots;

public class EchoBot(CommandFactory commandFactory) : ActivityHandler
{
    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        var text = turnContext.Activity.Text.Trim();
        var command = commandFactory.CreateCommand(text);

        await command.ExecuteAsync(turnContext, cancellationToken);
    }

    protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
    {
        const string welcomeText = "안녕하세요!\n\n봇 사용법이 궁금하시다면 'bot help' 또는 'bot -?' 를 입력해주세요.";

        foreach (var member in membersAdded)
        {
            if (member.Id != turnContext.Activity.Recipient.Id)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
            }
        }
    }
}
