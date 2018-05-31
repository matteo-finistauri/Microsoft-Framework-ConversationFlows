using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using Microsoft.Cognitive.LUIS;
using System.Collections.Generic;
using System.Linq;

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
        private readonly StateMachine<T, string, LanguageUnderstandingResult> stateMachine;

        /// <summary>
        /// The luis client
        /// </summary>
        private readonly LuisClient luisClient;

        /// <summary>
        /// The state action
        /// </summary>
        private readonly IStateBehavior<T> stateBehavior;

        /// <summary>
        /// The context
        /// </summary>
        private Dictionary<string, object> context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LuisStateFlowEngine" /> class.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="appKey">The application key.</param>
        /// <param name="initialState">The initial state.</param>
        /// <param name="luisConfiguration">The luis configuration.</param>
        /// <param name="stateBehavior">The state behavior.</param>
        /// <param name="context">The context.</param>
        public LuisStateFlowEngine(string appId, string appKey, T initialState,
            LuisFlowConfiguration<T> luisConfiguration,
            IStateBehavior<T> stateBehavior, Dictionary<string, object> context)
        {
            this.luisClient = new LuisClient(appId, appKey);
            this.stateMachine = new StateMachine<T, string, LanguageUnderstandingResult>(initialState);
            this.stateMachine.Transitions.AddRange(luisConfiguration.Transitions);
            this.stateBehavior = stateBehavior;
            this.context = context;
            this.stateBehavior?.ExecuteBehavior(initialState, context);
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
            this.stateBehavior?.ExecuteBehavior(nextState, context);
        }

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="luisClient">The luis client.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private LanguageUnderstandingResult RecognizeText(string text)
        {
            var result = this.luisClient.Predict(text).Result;
            var intent = result.TopScoringIntent;
            Dictionary<string, string> entities = new Dictionary<string, string>();
            foreach (var entity in result.Entities)
            {
                entities.Add(entity.Key, entity.Value.First().Value);
            }

            return new LanguageUnderstandingResult(intent.Name, entities);
        }

        #endregion Methods
    }
}