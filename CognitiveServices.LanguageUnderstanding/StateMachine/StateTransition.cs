using System;
using System.Collections.Generic;

namespace CognitiveServices.LanguageUnderstanding.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T">The state type.</typeparam>
    /// <typeparam name="U">The action type.</typeparam>
    /// <typeparam name="Y">The condition parameter type.</typeparam>
    public class StateTransition<T, U, Y>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StateTransition" /> class.
        /// </summary>
        /// <param name="currentState">State of the current.</param>
        /// <param name="nextState">State of the next.</param>
        /// <param name="action">The action.</param>
        /// <param name="isFinalState">if set to <c>true</c> [is final state].</param>
        /// <param name="condition">The condition.</param>
        public StateTransition(T currentState, T nextState, U action, bool isFinalState, Func<T, U, Y, bool> condition = null)
        {
            this.CurrentState = currentState;
            this.NextState = nextState;
            this.Action = action;
            this.IsFinalState = isFinalState;
            this.Condition = condition;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public T CurrentState { get; private set; }

        /// <summary>
        /// Gets the state of the next.
        /// </summary>
        /// <value>
        /// The state of the next.
        /// </value>
        public T NextState { get; private set; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public U Action { get; private set; }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public Func<T, U, Y, bool> Condition { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is final state.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is final state; otherwise, <c>false</c>.
        /// </value>
        public bool IsFinalState { get; set; }

        #endregion Properties

        #region Overridden methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            StateTransition<T, U, Y> other = obj as StateTransition<T, U, Y>;
            return other != null && this.CurrentState.Equals(other.CurrentState) && this.Action.Equals(other.Action);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 1781320185;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(CurrentState);
            hashCode = hashCode * -1521134295 + EqualityComparer<U>.Default.GetHashCode(Action);
            return hashCode;
        }

        #endregion Overridden methods
    }
}