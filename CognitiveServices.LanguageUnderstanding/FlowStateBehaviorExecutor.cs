using CognitiveServices.LanguageUnderstanding;
using CognitiveServices.LanguageUnderstanding.Attributes;
using CognitiveServices.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServices
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.IBehaviorExecutor{CognitiveServices.LanguageUnderstanding.FlowState}" />
    public class FlowStateBehaviorExecutor : IBehaviorExecutor<FlowState>
    {
        /// <summary>
        /// Executes the behavior.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="Exception"></exception>
        public void ExecuteBehavior(FlowState state, Dictionary<string, object> context)
        {
            if (state.BehaviorType == null)
            {
                return;
            }

            var behavior = Activator.CreateInstance(state.BehaviorType, state, context) as IStateBehavior;
            if (behavior == null)
            {
                throw new Exception($"Type '{state.BehaviorType}' is not {typeof(IStateBehavior).Name}.");
            }

            StateAttributesHelper.VerifyRequiredAttributes(state, context.Keys.ToArray());
            behavior.ExecuteBehavior();
            StateAttributesHelper.VerifyProvidedAttributes(state, context.Keys.ToArray());
        }
    }
}