using CognitiveServices.LanguageUnderstanding.DB;
using CognitiveServices.LanguageUnderstanding.Samples.Shared;
using CognitiveServices.LanguageUnderstanding.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Data.Entity;
using CognitiveServices.LanguageUnderstanding.Samples.Bot.Properties;

namespace CognitiveServices.LanguageUnderstanding.Bot.Dialogs
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.Bot.Dialogs.ILuisCommunicationManagerProvider" />
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
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public ILuisCommunicationManager Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        LuisFlowConfiguration<FlowState> luisConfiguration = null;
                        if (Settings.Default.LoadFromDatabase)
                        {
                            luisConfiguration = LoadFromDatabase();
                        }
                        else
                        {
                            luisConfiguration = LoadFromXmlFile();
                        }

                        var context = new Dictionary<string, object>()
                            {
                                { "outputStrings", StaticData.OutputStrings },
                                {"message", null },
                                {"context", null }
                            };
                        instance = new LuisCommunicationManager(
                         Settings.Default.LuisAppId, Settings.Default.LuisAppKey,
                         luisConfiguration, context);
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// Loads from XML file.
        /// </summary>
        /// <returns></returns>
        private static LuisFlowConfiguration<FlowState> LoadFromXmlFile()
        {
            LuisFlowConfiguration<FlowState> luisConfiguration;
            XmlSerializer serializer = new XmlSerializer(typeof(Xml.LuisConfiguration));
            using (TextReader reader = new StreamReader(Settings.Default.LuisConfigurationXmlFile))
            {
                var configuration = (Xml.LuisConfiguration)serializer.Deserialize(reader);
                luisConfiguration = XmlStatesConverter.Convert(configuration);
            }

            return luisConfiguration;
        }

        /// <summary>
        /// Loads from database.
        /// </summary>
        /// <returns></returns>
        private static LuisFlowConfiguration<FlowState> LoadFromDatabase()
        {
            ApplicationContext applicationContext = new ApplicationContext();
            var configuration = applicationContext.LuisConfigurations
                .Include(x => x.LuisFlowStates)
                .Include(x => x.LuisFlowStateTransitions)
                .Include(x => x.LuisFlowStateTransitions.Select(y => y.Condition))
                .First(x => x.ID == Settings.Default.LuisConfigurationId);
            return LuisDbDeserializer.StatesConverter.Convert(configuration);
        }
    }
}