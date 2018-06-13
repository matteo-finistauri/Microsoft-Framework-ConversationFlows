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

        /// <summary>
        /// Gets or sets the is entity equals.
        /// </summary>
        /// <value>
        /// The is entity equals.
        /// </value>
        public DbSet<IsEntityEquals> IsEntityEquals { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public DbSet<Condition> Conditions { get; set; }

        /// <summary>
        /// Gets or sets the luis flow state transitions.
        /// </summary>
        /// <value>
        /// The luis flow state transitions.
        /// </value>
        public DbSet<LuisFlowStateTransition> LuisFlowStateTransitions { get; set; }

        /// <summary>
        /// Gets or sets the or operators.
        /// </summary>
        /// <value>
        /// The or operators.
        /// </value>
        public DbSet<OrOperator> OrOperators { get; set; }

        /// <summary>
        /// Gets or sets the and operators.
        /// </summary>
        /// <value>
        /// The and operators.
        /// </value>
        public DbSet<AndOperator> AndOperators { get; set; }

        public DbSet<LuisConfiguration> LuisConfigurations { get; set; }
    }
}