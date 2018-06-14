using System;
using System.Text;
using System.Linq;

namespace CognitiveServices.LanguageUnderstanding.Conditions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.Conditions.IConditionOperator{T}" />
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

        /// <summary>
        /// Gets the name of the operator.
        /// </summary>
        /// <value>
        /// The name of the operator.
        /// </value>
        protected abstract string OperatorName { get; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var op in this.Operators)
            {
                sb.Append(op + " " + Operators + " ");
            }

            sb.Remove(sb.Length - this.OperatorName.Length - 1, this.OperatorName.Length + 1);
            return "(" + sb.ToString() + ")";
        }
    }
}