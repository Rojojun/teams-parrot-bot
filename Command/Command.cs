using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace EchoBot.Command
{
    public enum Command
    {
        Help,
        Show,   
        Store,
    }

    public static class CommandExtensions
    {
        public static async Task SendActivity(this Command command, ITurnContext turnContext)
        {
            var action = command.GetAction();
            await action(turnContext);
        }
    
        public static Command FindCommand(string command)
        {
            return command?.ToLower() switch
            {
                "help" or "?" => Command.Help,
                "show" => Command.Show,
                _ => Command.Store
            };
        }
        
        private static Func<ITurnContext, Task> GetAction(this Command command)
        {
            return command switch
            {
                Command.Help => async context =>
                    await context.SendActivityAsync(MessageFactory.Text("test")),
                Command.Show => async context =>
                    await context.SendActivityAsync(MessageFactory.Text("")),
                Command.Store => _ => Task.CompletedTask,
                _ => throw new ArgumentOutOfRangeException(nameof(command))
            };
        }
    }
}
