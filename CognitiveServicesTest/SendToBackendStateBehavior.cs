using CognitiveServicesTest.LanguageUnderstanding;
using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest
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