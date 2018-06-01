using CognitiveServices.LanguageUnderstanding;
using System;
using System.Collections.Generic;

namespace CognitiveServices
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.BaseStateBehavior" />
    public class SendToBackendStateBehavior : BaseStateBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendToBackendStateBehavior"/> class.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="context">The context.</param>
        public SendToBackendStateBehavior(FlowState state, Dictionary<string, object> context)
            : base(state, context)
        {
        }

        /// <summary>
        /// Executes the behavior.
        /// </summary>
        public override void ExecuteBehavior()
        {
            Console.WriteLine("Sent to the backend.");
        }
    }
}