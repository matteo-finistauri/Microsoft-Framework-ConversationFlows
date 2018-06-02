using System;
using System.Collections.Generic;

namespace CognitiveServices.LanguageUnderstanding.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="Y"></typeparam>
    public class MachineState<T, U, Y>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineState{T, U, Y}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public MachineState(T state)
        {
            this.State = state;
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public T State { get; }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        public List<MachineStateLink<T, U, Y>> Links { get; private set; } = new List<MachineStateLink<T, U, Y>>();
    }
}