using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.IStateBehavior{T}" />
    public class UserMessageEmitter<T> : IStateBehavior<T>
    {
        /// <summary>
        /// The behavior action
        /// </summary>
        private readonly Action<T> behaviorAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMessageEmitter{T}"/> class.
        /// </summary>
        public UserMessageEmitter(Action<T> behaviorAction)
        {
            this.behaviorAction = behaviorAction;
        }

        /// <summary>
        /// Executes the behavior.
        /// </summary>
        /// <param name="state">The state.</param>
        public void ExecuteBehavior(T state)
        {
            this.behaviorAction?.Invoke(state);
        }
    }
}