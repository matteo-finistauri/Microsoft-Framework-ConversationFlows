namespace CognitiveServices.LanguageUnderstanding.Bot.Dialogs
{
    /// <summary>
    /// This is useful in order to restore the same instance of the Luis Communication Manager
    /// since we can't keep the instance in the Dialog.
    /// </summary>
    public interface ILuisCommunicationManagerProvider
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        ILuisCommunicationManager Instance { get; }
    }
}