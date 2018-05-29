using Microsoft.Cognitive.LUIS;
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
    /// <typeparam name="T"></typeparam>
    public class LuisStateFlowEngine<T>
    {
        /// <summary>
        /// Occurs when [send to user].
        /// </summary>
        public event EventHandler<SendToUserEventArgs> SendToUser;

        /// <summary>
        /// The path
        /// </summary>
        private readonly StateMachine<T, string, LanguageUnderstandingRecognition> botPath;

        /// <summary>
        /// The luis client
        /// </summary>
        private readonly LuisClient luisClient;

        /// <summary>
        /// The output strings
        /// </summary>
        private readonly Dictionary<T, string> outputStrings;

        /// <summary>
        /// Initializes a new instance of the <see cref="LuisStateFlowEngine" /> class.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="appKey">The application key.</param>
        /// <param name="initialState">The initial state.</param>
        /// <param name="transitions">The transitions.</param>
        /// <param name="outputStrings">The output strings.</param>
        public LuisStateFlowEngine(string appId, string appKey, T initialState,
            StateTransition<T, string, LanguageUnderstandingRecognition>[] transitions,
            Dictionary<T, string> outputStrings)
        {
            this.luisClient = new LuisClient(appId, appKey);
            this.botPath = new StateMachine<T, string, LanguageUnderstandingRecognition>(initialState);
            this.botPath.Transitions.AddRange(transitions);
            this.outputStrings = outputStrings;
        }

        /// <summary>
        /// Elaborates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ElaborateMessage(string message)
        {
            var result = RecognizeText(luisClient, message);
            var nextState = this.botPath.MoveToNextState(result.EntityName, result);
            if (this.outputStrings.ContainsKey(nextState))
            {
                this.OnSendToUser(this.outputStrings[nextState]);
            }
        }

        /// <summary>
        /// Called when [send to user].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnSendToUser(string message)
        {
            this.SendToUser?.Invoke(this, new SendToUserEventArgs(message));
        }

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="luisClient">The luis client.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private static LanguageUnderstandingRecognition RecognizeText(LuisClient luisClient, string text)
        {
            var result = luisClient.Predict(text).Result;
            var intent = result.TopScoringIntent;
            Dictionary<string, string> entities = new Dictionary<string, string>();
            foreach (var entity in result.Entities)
            {
                entities.Add(entity.Key, entity.Value.First().Value);
            }

            return new LanguageUnderstandingRecognition(intent.Name, entities);
        }
    }

    public class SendToUserEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendToUserEventArgs" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SendToUserEventArgs(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }
    }
}