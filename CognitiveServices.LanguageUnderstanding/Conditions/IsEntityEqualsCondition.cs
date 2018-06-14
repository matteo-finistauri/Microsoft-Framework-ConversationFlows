using System;

namespace CognitiveServices.LanguageUnderstanding.Conditions
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
            if (conditionObject.Parameters.ContainsKey(this.EntityName))
            {
                return conditionObject.Parameters[this.EntityName].Equals(this.EntityValue, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.EntityName}='{this.EntityValue}'";
        }
    }
}