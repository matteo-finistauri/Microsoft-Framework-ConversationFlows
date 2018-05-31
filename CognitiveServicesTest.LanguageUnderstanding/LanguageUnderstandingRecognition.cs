using System.Collections.Generic;
using System.Text;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public class LanguageUnderstandingResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageUnderstandingResult" /> class.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="parameters">The parameters.</param>
        public LanguageUnderstandingResult(string entityName, Dictionary<string, string> parameters)
        {
            this.EntityName = entityName;
            this.Parameters = parameters;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public string EntityName { get; private set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public Dictionary<string, string> Parameters { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this.Parameters)
            {
                sb.Append(item.Key + "=" + item.Value + ";");
            }

            sb.Remove(sb.Length - 1, 1);
            return this.EntityName + "(" + sb.ToString() + ")";
        }

        #endregion Methods
    }
}