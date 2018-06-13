using System;
using System.Collections.Generic;

namespace CognitiveServices.LanguageUndestanding.Samples.Shared
{
    public class StaticData
    {
        /// <summary>
        /// The output strings
        /// </summary>
        public static Dictionary<string, string> OutputStrings { get; set; } = new Dictionary<string, string>()
        {
            { "InitialState", "Hello! This is the bot. What can I do for you?" },
            { "BuildingShelf", "Good! How long is your shelf?" },
            { "BuildingArmchair", "An armchair is usually provided as a unique piece." },
            { "BuildingSomethingElse", "I don't understand what you want to build. I can't do that." },
            { "TwoMeters", "All right! These are the steps you need to do..." },
            { "ErrorHandling", "I didn't understand what you said. Let's restart... What can I do for you?" }
        };
    }
}