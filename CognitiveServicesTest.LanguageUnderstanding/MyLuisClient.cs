using CognitiveServicesTest.LanguageUnderstanding.Conditions;
using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class MyLuisClient
    {
        #region Intents

        /// <summary>
        /// The build furniture intent
        /// </summary>
        private const string BUILD_FURNITURE_INTENT = "Build.Furniture";

        /// <summary>
        /// The shelf size intent
        /// </summary>
        private const string SHELF_SIZE_INTENT = "Shelf.Size";

        #endregion Intents

        #region Entities

        /// <summary>
        /// The furniture type entity
        /// </summary>
        private const string FURNITURE_TYPE_ENTITY = "FurnitureType";

        /// <summary>
        /// The builtin number entity
        /// </summary>
        private const string BUILTIN_NUMBER_ENTITY = "builtin.number";

        #endregion Entities

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
            new LuisFlowState<State>(State.InitialState, State.BuildingShelf, BUILD_FURNITURE_INTENT, false, new EntityBinding(FURNITURE_TYPE_ENTITY, "shelf")),
            new LuisFlowState<State>(State.BuildingShelf, State.TwoMeters, SHELF_SIZE_INTENT, false,
                new OrCondition<LanguageUnderstandingRecognition>(
                new EntityBinding(BUILTIN_NUMBER_ENTITY, "two"),
                new EntityBinding(BUILTIN_NUMBER_ENTITY, "2" ))),
            new LuisFlowState<State>(State.InitialState, State.BuildingArmchair, BUILD_FURNITURE_INTENT, false,
                new EntityBinding(FURNITURE_TYPE_ENTITY, "armchair" ))
        };

        /// <summary>
        /// The luis engine
        /// </summary>
        private readonly LuisStateFlowEngine<State> luisEngine;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyLuisClient"/> class.
        /// </summary>
        public MyLuisClient(string appId, string appKey)
        {
            this.luisEngine = new LuisStateFlowEngine<State>(appId, appKey, State.InitialState, transitions, new UserMessageEmitter<State>(x => this.SendToUser(this.outputStrings[x])));
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