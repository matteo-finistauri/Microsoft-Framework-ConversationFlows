using CognitiveServicesTest.LanguageUnderstanding;
using CognitiveServicesTest.LanguageUnderstanding.Conditions;
using CognitiveServicesTest.LanguageUnderstanding.DB;
using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using CognitiveServicesTest.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CognitiveServicesTest
{
    internal class Program
    {
        //#region Intents

        ///// <summary>
        ///// The build furniture intent
        ///// </summary>
        //private const string BUILD_FURNITURE_INTENT = "Build.Furniture";

        ///// <summary>
        ///// The shelf size intent
        ///// </summary>
        //private const string SHELF_SIZE_INTENT = "Shelf.Size";

        //#endregion Intents

        //#region Entities

        ///// <summary>
        ///// The furniture type entity
        ///// </summary>
        //private const string FURNITURE_TYPE_ENTITY = "FurnitureType";

        ///// <summary>
        ///// The builtin number entity
        ///// </summary>
        //private const string BUILTIN_NUMBER_ENTITY = "builtin.number";

        //#endregion Entities

        /// <summary>
        /// The output strings
        /// </summary>
        private static readonly Dictionary<string, string> outputStrings = new Dictionary<string, string>()
        {
            { "InitialState", "Hello! This is the bot. What can I do for you?" },
            { "BuildingShelf", "Good! How long is your shelf?" },
            { "TwoMeters", "Cool! Consider it built!" }
        };

        /// <summary>
        /// The transitions
        /// </summary>
        //private static readonly StateTransition<State, string, LanguageUnderstandingResult>[] transitions = {
        //    new LuisFlowStateTransition<State>(State.InitialState, State.BuildingShelf, BUILD_FURNITURE_INTENT, false, new IsEntityEquals(FURNITURE_TYPE_ENTITY, "shelf")),
        //    new LuisFlowStateTransition<State>(State.BuildingShelf, State.TwoMeters, SHELF_SIZE_INTENT, false,
        //        new OrCondition<LanguageUnderstandingResult>(new IConditionOperator<LanguageUnderstandingResult>[]{
        //        new IsEntityEquals(BUILTIN_NUMBER_ENTITY, "two"),
        //        new IsEntityEquals(BUILTIN_NUMBER_ENTITY, "2" )})),
        //    new LuisFlowStateTransition<State>(State.InitialState, State.BuildingArmchair, BUILD_FURNITURE_INTENT, false,
        //        new IsEntityEquals(FURNITURE_TYPE_ENTITY, "armchair" ))
        //};

        /// <summary>
        /// The filename
        /// </summary>
        private const string FILENAME = "Example.xml";

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            //ApplicationContext ctx = new ApplicationContext();
            //ctx.Database.Initialize(false);

            LuisConfiguration configuration = null;
            XmlSerializer serializer = new XmlSerializer(typeof(LuisConfiguration));
            using (TextReader reader = new StreamReader(FILENAME))
            {
                configuration = (LuisConfiguration)serializer.Deserialize(reader);
            }

            var luisConfiguration = StatesConverter.Convert(configuration);
            MyLuisClient client = new MyLuisClient(
                "a9777fd2-0c56-4a76-b3b4-740b387c05d5", "0c13af8b1228447bb2ce26e7be709940",
                luisConfiguration, outputStrings);
            client.Execute();
            Console.ReadKey();
        }
    }
}