using CognitiveServices.LanguageUnderstanding.Bot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot.Dialogs
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.Bot.Dialogs.LuisCommunicationDialog{System.Object}" />
    [Serializable]
    public class RootDialog : LuisCommunicationDialog<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootDialog"/> class.
        /// </summary>
        /// <param name="provider"></param>
        public RootDialog(ILuisCommunicationManagerProvider provider)
            : base(provider)
        {
        }

        /// <summary>
        /// The start of the code that represents the conversational dialog.
        /// </summary>
        /// <param name="context">The dialog context.</param>
        /// <returns>
        /// A task that represents the dialog start.
        /// </returns>
        public override Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Messages the received asynchronous.
        /// </summary>
        /// <param name="context">The dialog context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        protected async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            this.SetContext("context", context);
            this.ElaborateMessage(activity.Text);
            context.Wait(MessageReceivedAsync);
        }
    }
}