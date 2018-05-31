using System.Collections.Generic;

namespace CognitiveServicesTest.LanguageUnderstanding.Conditions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServicesTest.LanguageUnderstanding.IConditionOperator{T}" />
    public class AndCondition<T> : CombinationOperator<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndCondition{T}"/> class.
        /// </summary>
        public AndCondition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AndCondition{T}"/> class.
        /// </summary>
        /// <param name="operators">The operators.</param>
        public AndCondition(params IConditionOperator<T>[] operators)
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