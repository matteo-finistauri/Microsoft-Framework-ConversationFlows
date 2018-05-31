using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    [Requires(new string[] { "outputStrings" })]
    public class ConsoleWriterBehavior : IStateBehavior<FlowState>
    {
        /// <summary>
        /// Executes the behavior.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        public void ExecuteBehavior(FlowState state, Dictionary<string, object> context)
        {
            var outputStrings = (Dictionary<string, string>)context["outputStrings"];
            Console.WriteLine("Bot: " + outputStrings[state.Name]);
        }
    }
}