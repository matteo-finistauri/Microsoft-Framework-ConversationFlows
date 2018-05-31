using System;

namespace CognitiveServicesTest.LanguageUnderstanding.Attributes
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class RequiresAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAttribute"/> class.
        /// </summary>
        /// <param name="requiredObjects">The required objects.</param>
        public RequiresAttribute(string[] requiredObjects)
        {
            this.RequiredObjects = requiredObjects;
        }

        /// <summary>
        /// The required objects
        /// </summary>
        public string[] RequiredObjects { get; private set; }
    }
}