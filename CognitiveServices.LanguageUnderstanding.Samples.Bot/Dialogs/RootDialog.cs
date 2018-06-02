using CognitiveServices.LanguageUnderstanding.Bot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot.Dialogs
{
    [Serializable]
    public class RootDialog : LuisCommunicationDialog<object, StateProvider>
    {
        public override Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        protected async Task MessageReceivedAsync(IDialogContext dialogContext, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            this.SetContext("context", dialogContext);
            this.ElaborateMessage(activity.Text);
            dialogContext.Wait(MessageReceivedAsync);
        }
    }
}