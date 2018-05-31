using CognitiveServicesTest.LanguageUnderstanding;
using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest
{
    public class SendToBackendStateBehavior : IStateBehavior<FlowState>
    {
        private static void SendToBackend(FlowState state, Dictionary<string, object> context)
        {
        }

        public void ExecuteBehavior(FlowState state, Dictionary<string, object> context)
        {
            Console.WriteLine("Sent to the backend.");
        }
    }
}