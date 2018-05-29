namespace CognitiveServicesTest.LanguageUnderstanding.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStateBehavior<T>
    {
        /// <summary>
        /// Executes the behavior.
        /// </summary>
        /// <param name="state">The state.</param>
        void ExecuteBehavior(T state);
    }
}