using CognitiveServices.LanguageUnderstanding;
using CognitiveServices.LanguageUnderstanding.Attributes;
using System;
using System.Collections.Generic;

namespace CognitiveServices.LanguageUnderstanding.Samples.ConsoleApp
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.StateMachine.IStateBehavior{CognitiveServices.LanguageUnderstanding.FlowState}" />
    [Requires(new string[] { "outputStrings" })]
    public class ConsoleWriterBehavior : BaseStateBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleWriterBehavior"/> class.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="context">The context.</param>
        public ConsoleWriterBehavior(FlowState state, Dictionary<string, object> context)
            : base(state, context)
        {
        }

        /// <summary>
        /// Executes the behavior.
        /// </summary>
        public override void ExecuteBehavior()
        {
            var outputStrings = this.GetContextObject<Dictionary<string, string>>("outputStrings");
            Console.WriteLine("Bot: " + outputStrings[this.State.Name]);
        }
    }
}