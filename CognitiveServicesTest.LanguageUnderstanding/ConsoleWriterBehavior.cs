using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    public class ConsoleWriterBehavior : ExecuteActionStateBehavior<FlowState>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleWriterBehavior"/> class.
        /// </summary>
        public ConsoleWriterBehavior()
            : base((x, y) => SendToUser(x, y))
        {
        }

        /// <summary>
        /// Handles the SendToUser event of the LuisEngine control.
        /// </summary>
        /// <param name="message">The message.</param>
        private static object SendToUser(FlowState state, Dictionary<string, object> context)
        {
            var outputStrings = (Dictionary<string, string>)context["outputStrings"];
            Console.WriteLine("Bot: " + outputStrings[state.Name]);
            return context;
        }
    }
}