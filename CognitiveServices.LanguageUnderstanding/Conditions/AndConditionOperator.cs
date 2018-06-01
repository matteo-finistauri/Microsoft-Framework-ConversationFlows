namespace CognitiveServices.LanguageUnderstanding.Conditions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.Conditions.CombinationOperator{T}" />
    public class AndConditionOperator<T> : CombinationOperator<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndConditionOperator{T}"/> class.
        /// </summary>
        public AndConditionOperator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AndConditionOperator{T}"/> class.
        /// </summary>
        /// <param name="operators">The operators.</param>
        public AndConditionOperator(params IConditionOperator<T>[] operators)
            : base(operators)
        {
        }

        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate(T conditionObject)
        {
            foreach (var op in this.Operators)
            {
                if (!op.Evaluate(conditionObject))
                {
                    return false;
                }
            }

            return true;
        }
    }
}