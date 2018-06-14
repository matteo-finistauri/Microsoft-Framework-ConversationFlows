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
        public DbSet<LuisFlowState> FlowStates { get; set; }

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
        public DbSet<LuisFlowStateTransition> FlowStateTransitions { get; set; }

        /// <summary>
        /// Gets or sets the combinatorial operators.
        /// </summary>
        /// <value>
        /// The combinatorial operators.
        /// </value>
        public DbSet<CombinatorialOperator> CombinatorialOperators { get; set; }

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

        /// <summary>
        /// Gets or sets the luis configurations.
        /// </summary>
        /// <value>
        /// The luis configurations.
        /// </value>
        public DbSet<LuisConfiguration> LuisConfigurations { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LuisFlowStateTransition>()
                .HasRequired(t => t.CurrentState)
               .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<LuisFlowStateTransition>()
                .HasRequired(t => t.NextState)
               .WithMany().WillCascadeOnDelete(false);
        }
    }
}