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

        public MachineState<T, U, Y> DestinationState { get; }

        public U Action { get; }

        public Func<T, U, Y, bool> Condition { get; }
    }
}