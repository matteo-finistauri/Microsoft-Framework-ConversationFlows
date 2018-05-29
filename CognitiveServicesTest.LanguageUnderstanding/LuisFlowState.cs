using CognitiveServicesTest.LanguageUnderstanding.Conditions;
using CognitiveServicesTest.LanguageUnderstanding.StateMachine;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.StateTransition{T, System.String, CognitiveServicesTest.LanguageUnderstanding.LanguageUnderstandingRecognition}" />
    public class LuisFlowState<T> : StateTransition<T, string, LanguageUnderstandingRecognition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LuisFlowState{T}"/> class.
        /// </summary>
        /// <param name="currentState">State of the current.</param>
        /// <param name="nextState">State of the next.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="finalState">if set to <c>true</c> [final state].</param>
        /// <param name="entityConditions">The entity conditions.</param>
        public LuisFlowState(T currentState, T nextState, string intent, bool finalState, IConditionOperator<LanguageUnderstandingRecognition> entityConditions)
            : base(currentState, nextState, intent, finalState, (x, y, z) => entityConditions.Evaluate(z))
        {
        }
    }
}