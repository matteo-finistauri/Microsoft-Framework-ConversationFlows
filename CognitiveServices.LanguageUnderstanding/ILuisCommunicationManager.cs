using System.Collections.Generic;
using CognitiveServices.LanguageUnderstanding.StateMachine;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    public interface ILuisCommunicationManager
    {
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        int State { get; set; }

        /// <summary>
        /// Gets the state machine.
        /// </summary>
        /// <value>
        /// The state machine.
        /// </value>
        FiniteStateMachine<FlowState, string, LanguageUnderstandingResult> StateMachine { get; }

        /// <summary>
        /// Elaborates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ElaborateMessage(string message);

        /// <summary>
        /// Performs the static verification.
        /// </summary>
        /// <param name="initialKeys">The initial keys.</param>
        void PerformStaticVerification(IEnumerable<string> initialKeys);

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void SetContext(string key, object value);

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();
    }
}