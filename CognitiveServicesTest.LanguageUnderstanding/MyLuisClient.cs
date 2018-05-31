using CognitiveServicesTest.LanguageUnderstanding.Conditions;
using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class MyLuisClient
    {
        #region Fields

        /// <summary>
        /// The user inputs
        /// </summary>
        private readonly string[] userInputs = {
            "Hi Luis, can you help me to build my shelf?",
            "It's 2 meters."
        };

        /// <summary>
        /// The luis engine
        /// </summary>
        private readonly LuisStateFlowEngine<FlowState> luisEngine;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyLuisClient"/> class.
        /// </summary>
        public MyLuisClient(string appId, string appKey, LuisFlowConfiguration<FlowState> luisFlowConfiguration, Dictionary<string, string> outputStrings)
        {
            var initialState = luisFlowConfiguration.States.First(x => x.IsInitialState);
            Dictionary<string, object> context = new Dictionary<string, object>();
            context.Add("outputStrings", outputStrings);
            this.luisEngine = new LuisStateFlowEngine<FlowState>(appId, appKey, initialState, luisFlowConfiguration, new ClassInstantiatorBehavior<FlowState>(), context);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            foreach (var message in this.userInputs)
            {
                Console.WriteLine("User: " + message);
                this.luisEngine.ElaborateMessage(message);
            }
        }

        /// <summary>
        /// Handles the SendToUser event of the LuisEngine control.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendToUser(string message)
        {
            Console.WriteLine("Bot: " + message);
        }

        #endregion Methods
    }
}