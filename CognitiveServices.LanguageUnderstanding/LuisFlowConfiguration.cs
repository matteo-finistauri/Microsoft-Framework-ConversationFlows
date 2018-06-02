using CognitiveServices.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LuisFlowConfiguration<T>
    {
        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        /// <value>
        /// The states.
        /// </value>
        public List<T> States { get; set; }

        /// <summary>
        /// Gets or sets the transitions.
        /// </summary>
        /// <value>
        /// The transitions.
        /// </value>
        public List<StateTransition<T, string, LanguageUnderstandingResult>> Transitions { get; set; }
    }
}