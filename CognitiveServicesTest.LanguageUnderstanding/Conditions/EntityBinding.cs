namespace CognitiveServicesTest.LanguageUnderstanding.Conditions
{
    /// <summary>
    ///
    /// </summary>
    public class IsEntityEqualsCondition : IConditionOperator<LanguageUnderstandingResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsEntityEqualsCondition"/> class.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="entityValue">The entity value.</param>
        public IsEntityEqualsCondition(string entityName, string entityValue)
        {
            this.EntityName = entityName;
            this.EntityValue = entityValue;
        }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the entity value.
        /// </summary>
        /// <value>
        /// The entity value.
        /// </value>
        public string EntityValue { get; set; }

        /// <summary>
        /// Evaluates the specified condition object.
        /// </summary>
        /// <param name="conditionObject">The condition object.</param>
        /// <returns></returns>
        public bool Evaluate(LanguageUnderstandingResult conditionObject)
        {
            return conditionObject.Parameters[this.EntityName] == this.EntityValue;
        }
    }
}