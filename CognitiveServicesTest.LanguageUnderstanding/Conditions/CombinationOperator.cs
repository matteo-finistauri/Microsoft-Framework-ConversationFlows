namespace CognitiveServices.LanguageUnderstanding.Conditions
{
    public abstract class CombinationOperator<T> : IConditionOperator<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationOperator{T}"/> class.
        /// </summary>
        public CombinationOperator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationOperator{T}"/> class.
        /// </summary>
        /// <param name="operators">The operators.</param>
        public CombinationOperator(params IConditionOperator<T>[] operators)
        {
            this.Operators = operators;
        }

        /// <summary>
        /// Gets or sets the operators.
        /// </summary>
        /// <value>
        /// The operators.
        /// </value>
        public IConditionOperator<T>[] Operators { get; set; }

        /// <summary>
        /// Evaluates the specified condition object.
        /// </summary>
        /// <param name="conditionObject">The condition object.</param>
        /// <returns></returns>
        public abstract bool Evaluate(T conditionObject);
    }
}