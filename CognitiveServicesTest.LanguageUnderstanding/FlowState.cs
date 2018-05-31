using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class FlowState
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the behavior.
        /// </summary>
        /// <value>
        /// The type of the behavior.
        /// </value>
        public Type BehaviorType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initial state.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is initial state; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialState { get; set; }
    }
}