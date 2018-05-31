using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class FlowState
    {
        public string Name { get; set; }

        public Type BehaviorType { get; set; }

        public bool IsInitialState { get; set; }
    }
}