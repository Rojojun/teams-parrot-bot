// Generated with EchoBot .NET Template version v4.22.0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EchoBot.Command;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        private readonly ConcurrentDictionary<string, List<string>> _storage = new();
        
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var text = turnContext.Activity.Text.Trim();
            var command = CommandExtensions.FindCommand(text);
            var conversationId = turnContext.Activity.Conversation.Id;

            switch (command)
            {
                case Command.Command.Help:
                    await command.SendActivity(turnContext);
                    break;
                case Command.Command.Show:
                    var items = _storage.GetValueOrDefault(conversationId, []);

                    string message;
                    if (items.Count == 0)
                    {
                        message = "저장된 메시지가 없습니다.";
                    }
                    else
                    {
                        message = "저장된 메시지 목록:\n- " + string.Join("\n- ", items);
                    }
                    
                    await turnContext.SendActivityAsync(MessageFactory.Text(message), cancellationToken);
                    return;
                
                case Command.Command.Store:
                    _storage.AddOrUpdate(
                        conversationId,
                        [text],
                        (_, existing) =>
                        {
                            existing.Add(text);
                            return existing;
                        });
                    
                    await turnContext.SendActivityAsync(
                        MessageFactory.Text($"'{text}' (이)가 저장되었습니다."), 
                        cancellationToken);
                    return;
            }
            
            var replyText = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            const string welcomeText = "안녕하세요!\n\n봇 사용법이 궁금하시다면 bot -help, bot -? 를 입력해주세요.";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
