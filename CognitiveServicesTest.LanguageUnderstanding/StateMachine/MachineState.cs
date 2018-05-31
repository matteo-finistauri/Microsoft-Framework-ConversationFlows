using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding.StateMachine
{
    public class MachineState<T, U, Y>
    {
        public MachineState(T state)
        {
            this.State = state;
        }

        public T State { get; }

        public List<MachineStateDestination<T, U, Y>> LinkedStates { get; private set; } = new List<MachineStateDestination<T, U, Y>>();
    }
}