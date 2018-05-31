using System.Collections.Generic;

namespace CognitiveServicesTest.LanguageUnderstanding.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStateBehavior
    {
        /// <summary>
        /// Executes the behavior.
        /// </summary>
        void ExecuteBehavior();
    }
}