using System;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServices.LanguageUnderstanding.Attributes
{
    /// <summary>
    /// Provides some functionalities to work with the Provides and Requires
    /// attributes.
    /// </summary>
    public static class StateAttributesHelper
    {
        /// <summary>
        /// Verifies that the required objects keys defined in the state are actually
        /// provided (present in the keys array).
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="Exception">Object '" + item + "' is needed in the context for behavior of state '" + state.Name + "' but it's not provided.</exception>
        public static void VerifyRequiredAttributes(FlowState state, params string[] keys)
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
        /// Verifies that the provided objects keys defined from the state are actually
        /// provided with its behavior execution and put in the keys array.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="Exception">The context for behavior of state '" + state.Name + "' should provide the object '" + item + "' but it's not provided.</exception>
        public static void VerifyProvidedAttributes(FlowState state, params string[] keys)
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
        /// Gets the required objects keys defined through the Requires attribute
        /// in the specified type.
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
        /// Gets the provided objects keys defined through the Provides attribute
        /// in the specified type.
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
    }
}