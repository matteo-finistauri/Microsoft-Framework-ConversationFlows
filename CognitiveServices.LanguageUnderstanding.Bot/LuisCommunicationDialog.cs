using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CognitiveServices.LanguageUnderstanding.Bot.Dialogs
{
    /// <summary>
    /// This is a bot dialog to use the Luis Communcation Manager.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Microsoft.Bot.Builder.Dialogs.IDialog{T}" />
    [Serializable]
    public abstract class LuisCommunicationDialog<T> : IDialog<T>
    {
        #region Fields

        /// <summary>
        /// The provider
        /// </summary>
        private readonly ILuisCommunicationManagerProvider provider;

        /// <summary>
        /// The state
        /// </summary>
        private int state;

        /// <summary>
        /// The client
        /// </summary>
        [NonSerialized]
        private ILuisCommunicationManager client;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LuisCommunicationDialog{T}"/> class.
        /// </summary>
        public LuisCommunicationDialog(ILuisCommunicationManagerProvider provider)
        {
            this.provider = provider;
            // Needed to start from the initial state.
            this.state = this.Client.StateMachine.InitialState.State.Id;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        private ILuisCommunicationManager Client
        {
            get
            {
                if (this.client == null)
                {
                    this.client = provider.Instance;
                }

                return client;
            }
        }

        #endregion Properties

        #region Methods

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
        /// Sets a value in the context.
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

        #endregion Methods
    }
}