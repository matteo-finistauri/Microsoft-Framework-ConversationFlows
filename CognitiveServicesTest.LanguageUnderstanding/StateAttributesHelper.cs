using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public static class StateAttributesHelper
    {
        #region Attributes management

        /// <summary>
        /// Verifies the required attributes.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="Exception">Object '" + item + "' is needed in the context for behavior of state '" + state.Name + "' but it's not provided.</exception>
        public static void VerifyRequiredAttributes(FlowState state, IEnumerable<string> keys)
        {
            if (state.BehaviorType == null)
            {
                return;
            }

            IEnumerable<string> requiredObjects = GetRequiredObjectsKeys(state.BehaviorType);
            foreach (var item in requiredObjects)
            {
                if (!keys.Contains(item))
                {
                    throw new Exception("Object '" + item + "' is needed in the context for behavior of state '" + state.Name + "' but it's not provided.");
                }
            }
        }

        /// <summary>
        /// Verifies the provided attributes.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="Exception">The context for behavior of state '" + state.Name + "' should provide the object '" + item + "' but it's not provided.</exception>
        public static void VerifyProvidedAttributes(FlowState state, IEnumerable<string> keys)
        {
            if (state.BehaviorType == null)
            {
                return;
            }

            IEnumerable<string> providedObjects = GetProvidedObjectsKeys(state.BehaviorType);
            foreach (var item in providedObjects)
            {
                if (!keys.Contains(item))
                {
                    throw new Exception("The context for behavior of state '" + state.Name + "' should provide the object '" + item + "' but it's not provided.");
                }
            }
        }

        /// <summary>
        /// Gets the required objects.
        /// </summary>
        /// <param name="behaviorType">Type of the behavior.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetRequiredObjectsKeys(Type behaviorType)
        {
            if (behaviorType == null)
            {
                return new List<string>();
            }

            List<string> requiredObject = new List<string>();
            Dictionary<string, string> _dict = new Dictionary<string, string>();
            object[] attrs = behaviorType.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                var authAttr = attr as RequiresAttribute;
                if (authAttr != null)
                {
                    requiredObject.AddRange(authAttr.RequiredObjects);
                }
            }

            return requiredObject;
        }

        /// <summary>
        /// Gets the provided objects keys.
        /// </summary>
        /// <param name="behaviorType">Type of the behavior.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetProvidedObjectsKeys(Type behaviorType)
        {
            if (behaviorType == null)
            {
                return new List<string>();
            }

            List<string> requiredObject = new List<string>();
            Dictionary<string, string> _dict = new Dictionary<string, string>();
            object[] attrs = behaviorType.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                var authAttr = attr as ProvidesAttribute;
                if (authAttr != null)
                {
                    requiredObject.AddRange(authAttr.ProvidedObjects);
                }
            }

            return requiredObject;
        }

        #endregion Attributes management
    }
}