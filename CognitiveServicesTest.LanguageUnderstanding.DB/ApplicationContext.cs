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
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
        /// </summary>
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
    }
}