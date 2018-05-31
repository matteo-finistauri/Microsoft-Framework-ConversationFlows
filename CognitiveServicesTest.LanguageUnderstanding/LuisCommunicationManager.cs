using CognitiveServices.LanguageUnderstanding.Attributes;
using CognitiveServices.LanguageUnderstanding.StateMachine;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class LuisCommunicationManager
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
            this.luisEngine.Start();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Performs the static verification.
        /// </summary>
        /// <param name="initialKeys">The initial keys.</param>
        public void PerformStaticVerification(IEnumerable<string> initialKeys)
        {
            this.VerifyRequiredConsistency(this.luisEngine.StateMachine.InitialState, initialKeys);
        }

        /// <summary>
        /// Verifies the required consistency.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="existingKeys">The existing keys.</param>
        private void VerifyRequiredConsistency(MachineState<FlowState, string, LanguageUnderstandingResult> state, IEnumerable<string> existingKeys)
        {
            StateAttributesHelper.VerifyRequiredAttributes(state.State, existingKeys.ToArray());
            var providedObjects = StateAttributesHelper.GetProvidedObjectsKeys(state.State.BehaviorType);
            var newExistingKeys = new List<string>(existingKeys.ToArray());
            foreach (var destination in state.Links)
            {
                VerifyRequiredConsistency(destination.DestinationState, newExistingKeys);
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