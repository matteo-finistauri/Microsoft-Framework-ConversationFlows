namespace CognitiveServicesTest.LanguageUnderstanding.Conditions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrConditionOperator<T> : CombinationOperator<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrConditionOperator{T}"/> class.
        /// </summary>
        public OrConditionOperator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrConditionOperator{T}"/> class.
        /// </summary>
        /// <param name="operators">The operators.</param>
        public OrConditionOperator(params IConditionOperator<T>[] operators)
            : base(operators)
        {
        }

        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        /// <param name="conditionObject">The condition object.</param>
        /// <returns></returns>
        public override bool Evaluate(T conditionObject)
        {
            foreach (var op in this.Operators)
            {
                if (op.Evaluate(conditionObject))
                {
                    return true;
                }
            }

            return false;
        }
    }
}