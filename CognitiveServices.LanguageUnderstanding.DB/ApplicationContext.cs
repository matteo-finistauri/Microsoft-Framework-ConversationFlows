using System.Data.Entity;

namespace CognitiveServices.LanguageUnderstanding.DB
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