using CognitiveServices.LanguageUnderstanding.Conditions;
using CognitiveServices.LanguageUnderstanding.StateMachine;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.StateTransition{T, System.String, CognitiveServices.LanguageUnderstanding.LanguageUnderstandingResult}" />
    public class LuisFlowStateTransition<T> : StateTransition<T, string, LanguageUnderstandingResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LuisFlowStateTransition{T}"/> class.
        /// </summary>
        /// <param name="currentState">State of the current.</param>
        /// <param name="nextState">State of the next.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="finalState">if set to <c>true</c> [final state].</param>
        /// <param name="entityConditions">The entity conditions.</param>
        public LuisFlowStateTransition(T currentState, T nextState, string intent, bool finalState, IConditionOperator<LanguageUnderstandingResult> entityConditions)
            : base(currentState, nextState, intent, finalState, (x, y, z) => entityConditions.Evaluate(z))
        {
        }
    }
}