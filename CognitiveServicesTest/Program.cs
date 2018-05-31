using CognitiveServices.LanguageUnderstanding;
using CognitiveServices.LanguageUnderstanding.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CognitiveServices
{
    internal class Program
    {
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
        /// The user inputs
        /// </summary>
        private readonly static string[] userInputs = {
            "Hi Luis, can you help me to build my shelf?",
            "It's 2 meters."
        };

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

            var context = new Dictionary<string, object>
            {
                { "outputStrings", outputStrings }
            };

            var luisConfiguration = StatesConverter.Convert(configuration);
            LuisCommunicationManager client = new LuisCommunicationManager(
                "a9777fd2-0c56-4a76-b3b4-740b387c05d5", "0c13af8b1228447bb2ce26e7be709940",
                luisConfiguration, context);
            foreach (var message in userInputs)
            {
                Console.WriteLine("User: " + message);
                client.ElaborateMessage(message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        private static void Execute()
        {
        }
    }
}