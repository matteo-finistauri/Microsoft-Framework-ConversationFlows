using System;

namespace CognitiveServices.LanguageUnderstanding.Attributes
{
    /// <summary>
    /// Attribute to define the object keys provided by a state with the execution of
    /// its behavior.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class ProvidesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvidesAttribute"/> class.
        /// </summary>
        /// <param name="providedObjects">The provided objects.</param>
        public ProvidesAttribute(string[] providedObjects)
        {
            this.ProvidedObjects = providedObjects;
        }

        /// <summary>
        /// Gets the provided objects.
        /// </summary>
        /// <value>
        /// The provided objects.
        /// </value>
        public string[] ProvidedObjects { get; private set; }
    }
}