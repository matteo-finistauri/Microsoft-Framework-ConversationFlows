using CognitiveServices.LanguageUnderstanding;
using System;
using System.Collections.Generic;

namespace CognitiveServices
{
    public class SendToBackendStateBehavior : BaseStateBehavior
    {
        public SendToBackendStateBehavior(FlowState state, Dictionary<string, object> context)
            : base(state, context)
        {
        }

        public override void ExecuteBehavior()
        {
            Console.WriteLine("Sent to the backend.");
        }
    }
}