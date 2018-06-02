namespace CognitiveServices.LanguageUnderstanding.Bot.Dialogs
{
    /// <summary>
    ///
    /// </summary>
    public interface ILuisCommunicationManagerProvider
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        LuisCommunicationManager Instance { get; }
    }
}