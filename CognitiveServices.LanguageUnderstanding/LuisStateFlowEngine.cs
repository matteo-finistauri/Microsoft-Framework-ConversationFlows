using CognitiveServices.LanguageUnderstanding.StateMachine;
using Microsoft.Cognitive.LUIS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServices.LanguageUnderstanding
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LuisStateFlowEngine<T>
        where T : IState
    {
        #region Fields

        /// <summary>
        /// The state action
        /// </summary>
        private readonly IBehaviorExecutor<T> stateBehavior;

        /// <summary>
        /// The context
        /// </summary>
        private readonly Dictionary<string, object> context;

        /// <summary>
        /// The application identifier
        /// </summary>
        private readonly string appId;

        /// <summary>
        /// The application key
        /// </summary>
        private readonly string appKey;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LuisStateFlowEngine" /> class.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="appKey">The application key.</param>
        /// <param name="initialState">The initial state.</param>
        /// <param name="luisConfiguration">The luis configuration.</param>
        /// <param name="behaviorExecutor">The behavior executor.</param>
        /// <param name="context">The context.</param>
        public LuisStateFlowEngine(string appId, string appKey, T initialState,
            LuisFlowConfiguration<T> luisConfiguration,
            IBehaviorExecutor<T> behaviorExecutor,
            Dictionary<string, object> context)
        {
            this.appId = appId;
            this.appKey = appKey;
            this.StateMachine = new FiniteStateMachine<T, string, LanguageUnderstandingResult>(initialState, luisConfiguration.Transitions);
            this.stateBehavior = behaviorExecutor;
            this.context = context;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the state machine.
        /// </summary>
        /// <value>
        /// The state machine.
        /// </value>
        public FiniteStateMachine<T, string, LanguageUnderstandingResult> StateMachine { get; private set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public int State
        {
            get
            {
                return this.StateMachine.CurrentState.Id;
            }

            set
            {
                this.StateMachine.CurrentState = this.StateMachine.GetStateById(value);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetContext(string key, object value)
        {
            if (this.context.ContainsKey(key))
            {
                this.context[key] = value;
            }
            else
            {
                this.context.Add(key, value);
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.StateMachine.CurrentState = this.StateMachine.InitialState.State;
            this.stateBehavior.ExecuteBehavior(this.StateMachine.CurrentState, this.context);
        }

        /// <summary>
        /// Elaborates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ElaborateMessage(string message)
        {
            var recognizedData = this.RecognizeText(message);
            this.StateMachine.MoveToNextState(recognizedData.EntityName, recognizedData);
            this.stateBehavior?.ExecuteBehavior(this.StateMachine.CurrentState, context);
        }

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="luisClient">The luis client.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private LanguageUnderstandingResult RecognizeText(string text)
        {
            var luisClient = new LuisClient(appId, appKey);
            var result = luisClient.Predict(text).Result;
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