using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    public class LuisFlowConfiguration<T>
    {
        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        /// <value>
        /// The states.
        /// </value>
        public List<FlowState> States { get; set; }

        /// <summary>
        /// Gets or sets the transitions.
        /// </summary>
        /// <value>
        /// The transitions.
        /// </value>
        public List<StateTransition<T, string, LanguageUnderstandingResult>> Transitions { get; set; }
    }
}