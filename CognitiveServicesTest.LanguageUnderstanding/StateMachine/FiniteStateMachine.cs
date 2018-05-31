﻿using System;
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
    public class FiniteStateMachine<T, U, Y>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Process" /> class.
        /// </summary>
        /// <param name="initialState">The initial status.</param>
        /// <param name="transitions">The transitions.</param>
        public FiniteStateMachine(T initialState, IEnumerable<StateTransition<T, U, Y>> transitions)
        {
            this.CurrentState = initialState;
            this.Transitions = transitions;
            BuildMachineStates(initialState, transitions);
        }

        private void BuildMachineStates(T initialState, IEnumerable<StateTransition<T, U, Y>> transitions)
        {
            MachineState<T, U, Y> ms = new MachineState<T, U, Y>(initialState);
            ExploreMachineState(ms, transitions, new List<MachineState<T, U, Y>>() { ms });
            this.InitialState = ms;
        }

        private void ExploreMachineState(MachineState<T, U, Y> ms, IEnumerable<StateTransition<T, U, Y>> transitions, List<MachineState<T, U, Y>> mss)
        {
            var transits = this.Transitions.Where(x => x.CurrentState.Equals(ms.State));
            foreach (var transit in transits)
            {
                var destMs = mss.FirstOrDefault(x => x.State.Equals(transit.NextState));
                if (destMs == null)
                {
                    destMs = new MachineState<T, U, Y>(transit.NextState);
                    mss.Add(destMs);
                    ExploreMachineState(destMs, transitions, mss);
                }

                var destination = new MachineStateDestination<T, U, Y>(destMs, transit.Action, transit.Condition);
                ms.LinkedStates.Add(destination);
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the transitions.
        /// </summary>
        /// <value>
        /// The transitions.
        /// </value>
        public IEnumerable<StateTransition<T, U, Y>> Transitions { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public T CurrentState { get; private set; }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <value>
        /// The initial state.
        /// </value>
        public MachineState<T, U, Y> InitialState { get; private set; }

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