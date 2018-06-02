using System;

namespace CognitiveServices.LanguageUnderstanding.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="Y"></typeparam>
    public class MachineStateLink<T, U, Y>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineStateLink{T, U, Y}"/> class.
        /// </summary>
        /// <param name="destinationState">State of the destination.</param>
        /// <param name="action">The action.</param>
        /// <param name="condition">The condition.</param>
        public MachineStateLink(MachineState<T, U, Y> destinationState, U action, Func<T, U, Y, bool> condition = null)
        {
            this.DestinationState = destinationState;
            this.Action = action;
            this.Condition = condition;
        }

        /// <summary>
        /// Gets the state of the destination.
        /// </summary>
        /// <value>
        /// The state of the destination.
        /// </value>
        public MachineState<T, U, Y> DestinationState { get; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public U Action { get; }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public Func<T, U, Y, bool> Condition { get; }
    }
}