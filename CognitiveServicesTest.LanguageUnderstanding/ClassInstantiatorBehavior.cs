using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    public class ClassInstantiatorBehavior<T> : IStateBehavior<T>
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
        public void ExecuteBehavior(T state, Dictionary<string, object> context)
        {
            Type t = Type.GetType("CognitiveServicesTest.LanguageUnderstanding.ConsoleWriterBehavior, CognitiveServicesTest.LanguageUnderstanding");
            if (t == null)
            {
                throw new Exception("Type not found.");
            }
            var behavior = Activator.CreateInstance(t) as IStateBehavior<T>;
            if (behavior == null)
            {
                throw new Exception("Type is not IStateBehavior<" + typeof(T).Name + ">.");
            }

            behavior.ExecuteBehavior(state, context);
        }
    }
}