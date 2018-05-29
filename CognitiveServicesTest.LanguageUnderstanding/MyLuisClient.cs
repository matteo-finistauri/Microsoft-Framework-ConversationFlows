using Microsoft.Cognitive.LUIS;
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
        /// The output strings
        /// </summary>
        private readonly Dictionary<State, string> outputStrings = new Dictionary<State, string>()
        {
            { State.InitialState, "Hello! This is the bot. What can I do for you?" },
            { State.BuildingShelf, "Good! How long is your shelf?" },
            { State.TwoMeters, "Cool! Consider it built!" }
        };

        /// <summary>
        /// The transitions
        /// </summary>
        private readonly StateTransition<State, string, LanguageUnderstandingRecognition>[] transitions = {
            new StateTransition<State, string, LanguageUnderstandingRecognition>(State.InitialState, State.BuildingShelf, "Build.Furniture", false, (x, y, z) => IsEntityEquals(z, "FurnitureType", "shelf")),
            new StateTransition<State, string, LanguageUnderstandingRecognition>(State.BuildingShelf, State.TwoMeters, "Shelf.Size", false, (x, y, z) => IsEntityEquals(z, "builtin.number", "two") || IsEntityEquals(z, "builtin.number", "2")),
            new StateTransition<State, string, LanguageUnderstandingRecognition>(State.InitialState, State.BuildingArmchair, "Build.Furniture", false, (x, y, z) => IsEntityEquals(z, "FurnitureType", "armchair"))
        };

        /// <summary>
        /// The luis engine
        /// </summary>
        private readonly LuisStateFlowEngine<State> luisEngine;

        #endregion Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="MyLuisClient"/> class.
        /// </summary>
        public MyLuisClient(string appId, string appKey)
        {
            this.luisEngine = new LuisStateFlowEngine<State>(appId, appKey, State.InitialState, transitions, x => this.SendToUser(this.outputStrings[x]));
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            foreach (var message in userInputs)
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

        /// <summary>
        /// Determines whether [is entity equals] [the specified recognition].
        /// </summary>
        /// <param name="recognition">The recognition.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="entityValue">The entity value.</param>
        /// <returns>
        ///   <c>true</c> if [is entity equals] [the specified recognition]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsEntityEquals(LanguageUnderstandingRecognition recognition, string entityName, string entityValue)
        {
            if (!recognition.Parameters.TryGetValue(entityName, out string value))
            {
                return false;
            }

            return value == entityValue;
        }
    }
}