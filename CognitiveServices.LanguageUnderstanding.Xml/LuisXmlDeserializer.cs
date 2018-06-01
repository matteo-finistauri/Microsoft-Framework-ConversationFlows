using CognitiveServices.LanguageUnderstanding.Conditions;
using CognitiveServices.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CognitiveServices.LanguageUnderstanding.Xml
{
    /// <summary>
    ///
    /// </summary>
    public static class StatesConverter
    {
        /// <summary>
        /// Converts the specified configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static LuisFlowConfiguration<FlowState> Convert(LuisConfiguration configuration)
        {
            LuisFlowConfiguration<FlowState> config = new LuisFlowConfiguration<FlowState>();
            config.States = GetStates(configuration);
            var count = config.States.Count(x => x.IsInitialState);
            if (count == 0)
            {
                throw new Exception("An initial state is required.");
            }
            else if (count > 1)
            {
                throw new Exception("Only one initial state is needed.");
            }

            config.Transitions = GetTransitions(config.States, configuration);
            return config;
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Type '" + item.StateBehaviorClass + "' not found.
        /// or
        /// Type '" + item.StateBehaviorClass + "' is not IStateBehavior<" + typeof(FlowState).Name + ">.
        /// </exception>
        private static List<FlowState> GetStates(LuisConfiguration configuration)
        {
            var states = new List<FlowState>();
            foreach (var item in configuration.LuisFlowStates.List)
            {
                var state = new FlowState
                {
                    Name = item.Name,
                    IsInitialState = item.IsInitialState
                };
                if (item.StateBehaviorClass != null)
                {
                    Type type = Type.GetType(item.StateBehaviorClass);
                    if (type == null)
                    {
                        throw new Exception("Type '" + item.StateBehaviorClass + "' not found.");
                    }

                    var isImplementing = type.GetInterfaces().Contains(typeof(IStateBehavior));
                    if (!isImplementing)
                    {
                        throw new Exception("Type '" + item.StateBehaviorClass + "' is not IStateBehavior.");
                    }

                    state.BehaviorType = type;
                }

                states.Add(state);
            }

            return states;
        }

        /// <summary>
        /// Gets the transitions.
        /// </summary>
        /// <param name="flowStates">The flow states.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// </exception>
        private static List<StateTransition<FlowState, string, LanguageUnderstandingResult>> GetTransitions(List<FlowState> flowStates, LuisConfiguration configuration)
        {
            var transitions = new List<StateTransition<FlowState, string, LanguageUnderstandingResult>>();
            foreach (var luisFlowStateTransition in configuration.LuisFlowStateTransitions.List)
            {
                CognitiveServices.LanguageUnderstanding.Conditions.IConditionOperator<LanguageUnderstandingResult> op = null;
                if (luisFlowStateTransition.Condition != null)
                {
                    var inputAndOperator = luisFlowStateTransition.Condition.AndOperator;
                    if (inputAndOperator != null)
                    {
                        op = ProcessAndOperator(inputAndOperator);
                    }
                    var inputOrOperator = luisFlowStateTransition.Condition.OrOperator;
                    if (inputOrOperator != null)
                    {
                        op = ProcessOrOperator(inputOrOperator);
                    }
                    var entityEquals = luisFlowStateTransition.Condition.IsEntityEquals;
                    if (entityEquals != null)
                    {
                        op = ProcessIsEntityEqualsOperator(entityEquals);
                    }
                }

                var currentState = flowStates.FirstOrDefault(x => x.Name == luisFlowStateTransition.CurrentState);
                if (currentState == null)
                {
                    throw new Exception($"Current state '{luisFlowStateTransition.CurrentState}' is not defined among the states.");
                }

                var nextState = flowStates.FirstOrDefault(x => x.Name == luisFlowStateTransition.NextState);
                if (nextState == null)
                {
                    throw new Exception($"Next state '{luisFlowStateTransition.CurrentState}' is not defined among the states.");
                }

                var transition = new LuisFlowStateTransition<FlowState>(currentState, nextState, luisFlowStateTransition.Intent, luisFlowStateTransition.IsFinalState, op);
                transitions.Add(transition);
            }

            return transitions;
        }

        /// <summary>
        /// Processes the and operator.
        /// </summary>
        /// <param name="inputAndOperator">The input and operator.</param>
        /// <returns></returns>
        private static AndConditionOperator<LanguageUnderstandingResult> ProcessAndOperator(AndOperator inputAndOperator)
        {
            var andOperator = new CognitiveServices.LanguageUnderstanding.Conditions.AndConditionOperator<LanguageUnderstandingResult>();
            var operators = ProcessSuboperators(inputAndOperator);
            andOperator.Operators = operators.ToArray();
            return andOperator;
        }

        /// <summary>
        /// Processes the or operator.
        /// </summary>
        /// <param name="op">The op.</param>
        /// <returns></returns>
        private static IConditionOperator<LanguageUnderstandingResult> ProcessOrOperator(OrOperator op)
        {
            var orOperator = new CognitiveServices.LanguageUnderstanding.Conditions.OrConditionOperator<LanguageUnderstandingResult>();
            var operators = ProcessSuboperators(op);
            orOperator.Operators = operators.ToArray();
            return orOperator;
        }

        /// <summary>
        /// Processes the suboperators.
        /// </summary>
        /// <param name="combinatorialOperator">The combinatorial operator.</param>
        /// <returns></returns>
        private static List<IConditionOperator<LanguageUnderstandingResult>> ProcessSuboperators(CombinatorialOperator combinatorialOperator)
        {
            var operators = new List<IConditionOperator<LanguageUnderstandingResult>>();
            if (combinatorialOperator.AndOperators != null)
            {
                foreach (var op in combinatorialOperator.AndOperators)
                {
                    operators.Add(ProcessAndOperator(op));
                }
            }

            if (combinatorialOperator.OrOperators != null)
            {
                foreach (var op in combinatorialOperator.OrOperators)
                {
                    operators.Add(ProcessOrOperator(op));
                }
            }

            if (combinatorialOperator.IsEntityEquals != null)
            {
                foreach (var op in combinatorialOperator.IsEntityEquals)
                {
                    operators.Add(ProcessIsEntityEqualsOperator(op));
                }
            }

            return operators;
        }

        /// <summary>
        /// Processes the is entity equals operator.
        /// </summary>
        /// <param name="op">The op.</param>
        /// <returns></returns>
        private static IConditionOperator<LanguageUnderstandingResult> ProcessIsEntityEqualsOperator(IsEntityEquals op)
        {
            return new CognitiveServices.LanguageUnderstanding.Conditions.IsEntityEqualsCondition(op.Name, op.Value);
        }
    }
}