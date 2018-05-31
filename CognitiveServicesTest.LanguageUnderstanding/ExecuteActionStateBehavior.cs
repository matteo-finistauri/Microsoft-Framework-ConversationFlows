using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.StateMachine.IStateBehavior{T}" />
    public class ExecuteActionStateBehavior<T> : IStateBehavior<T>
    {
        /// <summary>
        /// The behavior action
        /// </summary>
        private readonly Func<T, Dictionary<string, object>, object> behaviorAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteActionStateBehavior{T}" /> class.
        /// </summary>
        /// <param name="behaviorAction">The behavior action.</param>
        public ExecuteActionStateBehavior(Func<T, Dictionary<string, object>, object> behaviorAction)
        {
            this.behaviorAction = behaviorAction;
        }

        /// <summary>
        /// Executes the behavior.
        /// </summary>
        /// <param name="state">The state.</param>
        public void ExecuteBehavior(T state, Dictionary<string, object> context)
        {
            this.behaviorAction?.Invoke(state, context);
        }
    }
}