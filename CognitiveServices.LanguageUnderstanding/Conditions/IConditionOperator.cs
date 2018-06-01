namespace CognitiveServices.LanguageUnderstanding.Conditions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConditionOperator<T>
    {
        /// <summary>
        /// Evaluates the specified condition object.
        /// </summary>
        /// <param name="conditionObject">The condition object.</param>
        /// <returns></returns>
        bool Evaluate(T conditionObject);
    }
}