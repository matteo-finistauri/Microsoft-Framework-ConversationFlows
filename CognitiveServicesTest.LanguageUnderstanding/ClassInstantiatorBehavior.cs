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
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.StateMachine.IStateBehavior{CognitiveServicesTest.LanguageUnderstanding.FlowState}" />
    public class ClassInstantiatorBehavior : IStateBehavior<FlowState>
    {
        /// <summary>
        /// Executes the behavior.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Type not found.
        /// or
        /// Type is not IStateBehavior<" + typeof(T).Name + ">.
        /// </exception>
        public void ExecuteBehavior(FlowState state, Dictionary<string, object> context)
        {
            if (state.BehaviorType == null)
            {
                return;
            }

            var behavior = Activator.CreateInstance(state.BehaviorType) as IStateBehavior<FlowState>;
            if (behavior == null)
            {
                throw new Exception("Type is not IStateBehavior<" + typeof(FlowState).Name + ">.");
            }

            StateAttributesHelper.VerifyRequiredAttributes(state, context.Keys);
            behavior.ExecuteBehavior(state, context);
            StateAttributesHelper.VerifyProvidedAttributes(state, context.Keys);
        }
    }
}