using System;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServicesTest.LanguageUnderstanding.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T">The state type.</typeparam>
    /// <typeparam name="U">The action type.</typeparam>
    /// <typeparam name="Y">The condition parameter type.</typeparam>
    public class StateMachine<T, U, Y>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Process" /> class.
        /// </summary>
        /// <param name="initialStatus">The initial status.</param>
        public StateMachine(T initialStatus)
        {
            this.CurrentState = initialStatus;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the transitions.
        /// </summary>
        /// <value>
        /// The transitions.
        /// </value>
        public List<StateTransition<T, U, Y>> Transitions { get; } = new List<StateTransition<T, U, Y>>();

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public T CurrentState { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Moves the state of to next.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="conditionObject">The condition object.</param>
        /// <returns></returns>
        public T MoveToNextState(U action, Y conditionObject)
        {
            this.CurrentState = GetNext(action, conditionObject);
            return this.CurrentState;
        }

        /// <summary>
        /// Gets the next.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="conditionObject">The condition object.</param>
        /// <returns></returns>
        private T GetNext(U action, Y conditionObject)
        {
            var validTransitions = this.Transitions.Where(
                x => x.CurrentState.Equals(this.CurrentState) &&
                x.Action.Equals(action) &&
                (x.Condition == null || x.Condition(this.CurrentState, action, conditionObject))
                ).Select(x => x.NextState);
            if (validTransitions.Count() == 0)
            {
                throw new Exception("No transitions found for " + this.CurrentState + " --> " + action);
            }
            else if (validTransitions.Count() > 1)
            {
                throw new Exception("The transition for " + this.CurrentState + " --> " + action + " is not unique");
            }

            return validTransitions.First();
        }

        #endregion Methods
    }
}