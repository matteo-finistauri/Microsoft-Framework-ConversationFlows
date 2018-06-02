using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot.Dialogs
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Microsoft.Bot.Builder.Dialogs.IDialog{T}" />
    [Serializable]
    public abstract class LuisCommunicationDialog<T> : IDialog<T>
    {
        /// <summary>
        /// The state
        /// </summary>
        private int state;

        /// <summary>
        /// The client
        /// </summary>
        [NonSerialized]
        private LuisCommunicationManager client;

        /// <summary>
        /// Initializes a new instance of the <see cref="LuisCommunicationDialog{T}"/> class.
        /// </summary>
        public LuisCommunicationDialog()
        {
            this.state = this.Client.StateMachine.InitialState.State.Id;
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        private LuisCommunicationManager Client
        {
            get
            {
                if (this.client == null)
                {
                    this.client = StateProvider.Instance;
                }

                return client;
            }
        }

        /// <summary>
        /// Elaborates the message.
        /// </summary>
        /// <param name="text">The text.</param>
        protected void ElaborateMessage(string text)
        {
            this.Client.State = this.state;
            this.Client.ElaborateMessage(text);
            this.state = client.State;
        }

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected void SetContext<Y>(string key, Y value)
        {
            this.Client.SetContext("context", value);
        }

        /// <summary>
        /// The start of the code that represents the conversational dialog.
        /// </summary>
        /// <param name="context">The dialog context.</param>
        /// <returns>
        /// A task that represents the dialog start.
        /// </returns>
        public abstract Task StartAsync(IDialogContext context);
    }
}