using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.IBehaviorExecutor{CognitiveServicesTest.LanguageUnderstanding.FlowState}" />
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.StateMachine.IStateBehavior{CognitiveServicesTest.LanguageUnderstanding.FlowState}" />
    public class ClassInstantiator : IBehaviorExecutor<FlowState>
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