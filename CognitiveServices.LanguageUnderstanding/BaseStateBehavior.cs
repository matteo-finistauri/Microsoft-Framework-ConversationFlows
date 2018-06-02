using CognitiveServices.LanguageUnderstanding.Attributes;
using CognitiveServices.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CognitiveServices.LanguageUnderstanding.StateMachine.IStateBehavior" />
    public abstract class BaseStateBehavior : IStateBehavior
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly Dictionary<string, object> context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseStateBehavior{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseStateBehavior(FlowState state, Dictionary<string, object> context)
        {
            this.State = state;
            this.context = context;
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        protected FlowState State { get; }

        /// <summary>
        /// Sets the context object.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="obj">The object.</param>
        protected void SetContextObject<U>(string key, U obj)
            where U : class
        {
            StateAttributesHelper.VerifyProvidedAttributes(this.State, key);
            if (this.context.ContainsKey(key))
            {
                this.context[key] = obj;
            }
            else
            {
                this.context.Add(key, obj);
            }
        }

        /// <summary>
        /// Gets the context object.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException">Cast to type " + typeof(U).FullName + " is incorrect.</exception>
        protected U GetContextObject<U>(string key)
            where U : class
        {
            //StateAttributesHelper.VerifyRequiredAttributes(this.State, key);
            if (this.context[key] == null)
            {
                return null;
            }

            if (!(this.context[key] is U obj))
            {
                throw new InvalidCastException($"Cast of context object with key '{key}' to type {typeof(U).FullName} is incorrect.");
            }

            return obj;
        }

        /// <summary>
        /// Executes the behavior.
        /// </summary>
        public abstract void ExecuteBehavior();
    }
}