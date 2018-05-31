using System;

namespace CognitiveServicesTest.LanguageUnderstanding.Attributes
{
    /// <summary>
    ///
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