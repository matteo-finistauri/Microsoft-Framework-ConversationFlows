﻿using System.Collections.Generic;

namespace CognitiveServicesTest.LanguageUnderstanding.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStateBehavior<T>
    {
        /// <summary>
        /// Executes the behavior.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="context">The context.</param>
        void ExecuteBehavior(T state, Dictionary<string, object> context);
    }
}