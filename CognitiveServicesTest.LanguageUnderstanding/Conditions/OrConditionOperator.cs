namespace CognitiveServicesTest.LanguageUnderstanding.Conditions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.IConditionOperator{T}" />
    public class OrCondition<T> : IConditionOperator<T>
    {
        /// <summary>
        /// The operator1
        /// </summary>
        private readonly IConditionOperator<T> operator1;

        /// <summary>
        /// The operator2
        /// </summary>
        private readonly IConditionOperator<T> operator2;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrCondition" /> class.
        /// </summary>
        /// <param name="operator1">The operator1.</param>
        /// <param name="operator2">The operator2.</param>
        public OrCondition(IConditionOperator<T> operator1, IConditionOperator<T> operator2)
        {
            this.operator1 = operator1;
            this.operator2 = operator2;
        }

        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        /// <returns></returns>
        public bool Evaluate(T conditionObject)
        {
            return this.operator1.Evaluate(conditionObject) || this.operator2.Evaluate(conditionObject);
        }
    }
}