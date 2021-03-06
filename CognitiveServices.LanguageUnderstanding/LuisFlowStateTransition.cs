﻿using CognitiveServices.LanguageUnderstanding.Conditions;
using CognitiveServices.LanguageUnderstanding.StateMachine;
using System;
using System.Text;

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
        /// Initializes a new instance of the <see cref="LuisFlowStateTransition{T}" /> class.
        /// </summary>
        /// <param name="currentState">State of the current.</param>
        /// <param name="nextState">State of the next.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="finalState">if set to <c>true</c> [final state].</param>
        /// <param name="activationCondition">The activation condition.</param>
        public LuisFlowStateTransition(T currentState, T nextState, string intent, bool finalState, IConditionOperator<LanguageUnderstandingResult> activationCondition)
        : base(currentState, nextState, intent, finalState, (x, y, z) => activationCondition != null ? activationCondition.Evaluate(z) : true)
        {
            this.ActivationCondition = activationCondition;
        }

        /// <summary>
        /// The activation condition
        /// </summary>
        public IConditionOperator<LanguageUnderstandingResult> ActivationCondition { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(CurrentState + " -> " + NextState);
            sb.Append(" condition=" + this.ActivationCondition);
            return sb.ToString();
        }
    }
}