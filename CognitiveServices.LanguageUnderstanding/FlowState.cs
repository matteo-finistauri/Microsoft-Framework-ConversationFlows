using System;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class FlowState : IState
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}