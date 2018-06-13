﻿using CognitiveServices.LanguageUnderstanding.Samples.Shared;
using CognitiveServices.LanguageUnderstanding.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CognitiveServices.LanguageUnderstanding.Bot.Dialogs
{
    public class StateProvider : ILuisCommunicationManagerProvider
    {
        /// <summary>
        /// The lock object
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        /// The instance
        /// </summary>
        private static LuisCommunicationManager instance;

        /// <summary>
        /// The filename
        /// </summary>
        private const string FILENAME = @"C:\Users\matteo.finistauri\source\repos\CognitiveServicesTest\CognitiveServices.LanguageUnderstanding.Samples.Bot\Example.xml";

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public LuisCommunicationManager Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        //LuisConfiguration configuration = null;
                        XmlSerializer serializer = new XmlSerializer(typeof(LuisConfiguration));
                        using (TextReader reader = new StreamReader(FILENAME))
                        {
                            var configuration = (LuisConfiguration)serializer.Deserialize(reader);
                            var luisConfiguration = StatesConverter.Convert(configuration);
                            var context = new Dictionary<string, object>()
                            {
                                { "outputStrings", StaticData.OutputStrings },
                                {"message", null },
                                {"context", null }
                            };
                            instance = new LuisCommunicationManager(
                             "a9777fd2-0c56-4a76-b3b4-740b387c05d5", "0c13af8b1228447bb2ce26e7be709940",
                             luisConfiguration, context);
                        }
                    }

                    return instance;
                }
            }
        }
    }
}