using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace EchoBot.Command;

public class HelpCommand : ICommand
{
    public async Task ExecuteAsync(ITurnContext turnContext, CancellationToken cancellationToken)
    {
        const string helpMessage = """
            사용 가능한 명령어:
            - bot help, bot -help, bot -? : 도움말 표시
            - bot show : 저장된 메시지 목록 표시
            - 그 외 모든 메시지 : 메시지 저장
            """;

        await turnContext.SendActivityAsync(MessageFactory.Text(helpMessage), cancellationToken);
    }
}
