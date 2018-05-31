using CognitiveServicesTest.LanguageUnderstanding.Conditions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding.DB
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
             : base("name=CognitiveServicesTest.LanguageUnderstanding.DB.Properties.Settings.ApplicationConnectionString")
        {
        }

        /// <summary>
        /// Gets or sets the flow states.
        /// </summary>
        /// <value>
        /// The flow states.
        /// </value>
        public DbSet<FlowState> FlowStates { get; set; }

        /// <summary>
        /// Gets or sets the intents.
        /// </summary>
        /// <value>
        /// The intents.
        /// </value>
        public DbSet<Intent> Intents { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public DbSet<Entity> Entities { get; set; }

        ///// <summary>
        ///// Gets or sets the or conditions.
        ///// </summary>
        ///// <value>
        ///// The or conditions.
        ///// </value>
        //public DbSet<OrCondition<LanguageUnderstandingResult>> OrConditions { get; set; }

        ///// <summary>
        ///// Gets or sets the is entity equals conditions.
        ///// </summary>
        ///// <value>
        ///// The is entity equals conditions.
        ///// </value>
        //public DbSet<IsEntityEqualsCondition> IsEntityEqualsConditions { get; set; }

        ///// <summary>
        ///// Gets or sets the language understanding results.
        ///// </summary>
        ///// <value>
        ///// The language understanding results.
        ///// </value>
        //public DbSet<LanguageUnderstandingResult> LanguageUnderstandingResults { get; set; }
    }
}