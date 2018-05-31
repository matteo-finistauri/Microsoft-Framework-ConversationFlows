using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
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
            var behavior = Activator.CreateInstance(state.BehaviorType) as IStateBehavior<FlowState>;
            if (behavior == null)
            {
                throw new Exception("Type is not IStateBehavior<" + typeof(FlowState).Name + ">.");
            }

            IEnumerable<string> requiredObjects = GetRequiredObjects(state.BehaviorType);
            foreach (var item in requiredObjects)
            {
                if (!context.ContainsKey(item))
                {
                    throw new Exception("Object '" + item + "' is needed in the context for behavior of state '" + state.Name + "' but it's not provided.");
                }
            }

            behavior.ExecuteBehavior(state, context);
        }

        private IEnumerable<string> GetRequiredObjects(Type behaviorType)
        {
            List<string> requiredObject = new List<string>();
            Dictionary<string, string> _dict = new Dictionary<string, string>();
            object[] attrs = behaviorType.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                var authAttr = attr as RequiresAttribute;
                if (authAttr != null)
                {
                    requiredObject.AddRange(authAttr.RequiredObjects);
                }
            }

            return requiredObject;
        }
    }
}