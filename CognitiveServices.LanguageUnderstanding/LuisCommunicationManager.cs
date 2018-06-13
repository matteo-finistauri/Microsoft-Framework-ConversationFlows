using CognitiveServices.LanguageUnderstanding.Attributes;
using CognitiveServices.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class LuisCommunicationManager : ILuisCommunicationManager
    {
        #region Fields

        /// <summary>
        /// The luis engine
        /// </summary>
        private readonly LuisStateFlowEngine<FlowState> luisEngine;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LuisCommunicationManager"/> class.
        /// </summary>
        public LuisCommunicationManager(string appId, string appKey, LuisFlowConfiguration<FlowState> luisFlowConfiguration, Dictionary<string, object> context)
        {
            var initialState = luisFlowConfiguration.States.Single(x => x.IsInitialState);
            this.luisEngine = new LuisStateFlowEngine<FlowState>(appId, appKey, initialState, luisFlowConfiguration, new FlowStateBehaviorExecutor(), context);
            this.PerformStaticVerification(context.Keys);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.luisEngine.Start();
        }

        #endregion Constructors

        /// <summary>
        /// Gets the state machine.
        /// </summary>
        /// <value>
        /// The state machine.
        /// </value>
        public FiniteStateMachine<FlowState, string, LanguageUnderstandingResult> StateMachine
        {
            get
            {
                return this.luisEngine.StateMachine;
            }
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public int State
        {
            get
            {
                return this.luisEngine.State;
            }
            set
            {
                this.luisEngine.State = value;
            }
        }

        #region Methods

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetContext(string key, object value)
        {
            this.luisEngine.SetContext(key, value);
        }

        /// <summary>
        /// Performs the static verification.
        /// </summary>
        /// <param name="initialKeys">The initial keys.</param>
        public void PerformStaticVerification(IEnumerable<string> initialKeys)
        {
            this.VerifyRequiredConsistency(this.luisEngine.StateMachine.InitialState, initialKeys, new List<MachineState<FlowState, string, LanguageUnderstandingResult>>());
        }

        /// <summary>
        /// Verifies the required consistency.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="existingKeys">The existing keys.</param>
        private void VerifyRequiredConsistency(MachineState<FlowState, string, LanguageUnderstandingResult> state, IEnumerable<string> existingKeys, List<MachineState<FlowState, string, LanguageUnderstandingResult>> statesAlreadySeen)
        {
            StateAttributesHelper.VerifyRequiredAttributes(state.State, existingKeys.ToArray());
            statesAlreadySeen.Add(state);
            var providedObjects = StateAttributesHelper.GetProvidedObjectsKeys(state.State.BehaviorType);
            var newExistingKeys = new List<string>(existingKeys.ToArray());
            foreach (var destination in state.Links)
            {
                if (!statesAlreadySeen.Contains(destination.DestinationState))
                {
                    VerifyRequiredConsistency(destination.DestinationState, newExistingKeys, statesAlreadySeen);
                }
            }
        }

        /// <summary>
        /// Elaborates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ElaborateMessage(string message)
        {
            this.luisEngine.ElaborateMessage(message);
        }

        #endregion Methods
    }
}