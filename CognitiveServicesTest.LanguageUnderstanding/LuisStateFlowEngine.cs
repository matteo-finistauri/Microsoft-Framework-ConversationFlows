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
        #region Fields

        /// <summary>
        /// The path
        /// </summary>
        private readonly StateMachine<T, string, LanguageUnderstandingRecognition> stateMachine;

        /// <summary>
        /// The luis client
        /// </summary>
        private readonly LuisClient luisClient;

        /// <summary>
        /// The state action
        /// </summary>
        private readonly Action<T> stateAction;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LuisStateFlowEngine" /> class.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="appKey">The application key.</param>
        /// <param name="initialState">The initial state.</param>
        /// <param name="transitions">The transitions.</param>
        /// <param name="outputStrings">The output strings.</param>
        public LuisStateFlowEngine(string appId, string appKey, T initialState,
            IEnumerable<StateTransition<T, string, LanguageUnderstandingRecognition>> transitions,
            Action<T> stateAction)
        {
            this.luisClient = new LuisClient(appId, appKey);
            this.stateMachine = new StateMachine<T, string, LanguageUnderstandingRecognition>(initialState);
            this.stateMachine.Transitions.AddRange(transitions);
            this.stateAction = stateAction;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Elaborates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ElaborateMessage(string message)
        {
            var recognizedData = this.RecognizeText(message);
            var nextState = this.stateMachine.MoveToNextState(recognizedData.EntityName, recognizedData);
            this.stateAction?.Invoke(nextState);
        }

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="luisClient">The luis client.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private LanguageUnderstandingRecognition RecognizeText(string text)
        {
            var result = this.luisClient.Predict(text).Result;
            var intent = result.TopScoringIntent;
            Dictionary<string, string> entities = new Dictionary<string, string>();
            foreach (var entity in result.Entities)
            {
                entities.Add(entity.Key, entity.Value.First().Value);
            }

            return new LanguageUnderstandingRecognition(intent.Name, entities);
        }

        #endregion Methods
    }
}