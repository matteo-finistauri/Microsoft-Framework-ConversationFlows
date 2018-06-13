using System.Collections.Generic;
using CognitiveServices.LanguageUnderstanding.StateMachine;

namespace CognitiveServices.LanguageUnderstanding
{
    public interface ILuisCommunicationManager
    {
        int State { get; set; }
        FiniteStateMachine<FlowState, string, LanguageUnderstandingResult> StateMachine { get; }

        void ElaborateMessage(string message);

        void PerformStaticVerification(IEnumerable<string> initialKeys);

        void SetContext(string key, object value);

        void Start();
    }
}