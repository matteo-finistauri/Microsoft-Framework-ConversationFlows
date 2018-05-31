using CognitiveServicesTest.LanguageUnderstanding;
using CognitiveServicesTest.LanguageUnderstanding.Conditions;
using CognitiveServicesTest.LanguageUnderstanding.StateMachine;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace CognitiveServicesTest.Serialization
{
    [XmlRoot(ElementName = "luisFlowState")]
    public class LuisFlowState
    {
        [XmlElement(ElementName = "stateBehaviorClass")]
        public string StateBehaviorClass { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "isInitialState")]
        public bool IsInitialState { get; set; }
    }

    [XmlRoot(ElementName = "luisFlowStates")]
    public class LuisFlowStates
    {
        [XmlElement(ElementName = "luisFlowState")]
        public List<LuisFlowState> List { get; set; }
    }

    [XmlRoot(ElementName = "isEntityEquals")]
    public class IsEntityEquals
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "condition")]
    public class Condition
    {
        [XmlElement(ElementName = "isEntityEquals")]
        public IsEntityEquals IsEntityEquals { get; set; }

        [XmlElement(ElementName = "orOperator")]
        public OrOperator OrOperator { get; set; }

        [XmlElement(ElementName = "andOperator")]
        public AndOperator AndOperator { get; set; }
    }

    [XmlRoot(ElementName = "luisFlowStateTransition")]
    public class LuisFlowStateTransition
    {
        [XmlElement(ElementName = "condition")]
        public Condition Condition { get; set; }

        [XmlAttribute(AttributeName = "currentState")]
        public string CurrentState { get; set; }

        [XmlAttribute(AttributeName = "nextState")]
        public string NextState { get; set; }

        [XmlAttribute(AttributeName = "intent")]
        public string Intent { get; set; }

        [XmlAttribute(AttributeName = "isFinalState")]
        public bool IsFinalState { get; set; }
    }

    public class CombinatorialOperator
    {
        [XmlElement(ElementName = "isEntityEquals")]
        public List<IsEntityEquals> IsEntityEquals { get; set; }

        [XmlElement(ElementName = "orOperator")]
        public List<OrOperator> OrOperators { get; set; }

        [XmlElement(ElementName = "andOperator")]
        public List<AndOperator> AndOperators { get; set; }
    }

    [XmlRoot(ElementName = "orOperator")]
    public class OrOperator : CombinatorialOperator
    {
    }

    [XmlRoot(ElementName = "andOperator")]
    public class AndOperator : CombinatorialOperator
    {
    }

    [XmlRoot(ElementName = "luisFlowStateTransitions")]
    public class LuisFlowStateTransitions
    {
        [XmlElement(ElementName = "luisFlowStateTransition")]
        public List<LuisFlowStateTransition> List { get; set; }
    }

    [XmlRoot(ElementName = "luisConfiguration")]
    public class LuisConfiguration
    {
        [XmlElement(ElementName = "luisFlowStates")]
        public LuisFlowStates LuisFlowStates { get; set; }

        [XmlElement(ElementName = "luisFlowStateTransitions")]
        public LuisFlowStateTransitions LuisFlowStateTransitions { get; set; }
    }

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
            config.Transitions = GetTransitions(config.States, configuration);

            return config;
        }

        private static List<FlowState> GetStates(LuisConfiguration configuration)
        {
            var states = new List<FlowState>();
            foreach (var item in configuration.LuisFlowStates.List)
            {
                var state = new FlowState();
                state.Name = item.Name;
                state.IsInitialState = item.IsInitialState;
                Type t = Type.GetType(item.StateBehaviorClass);
                if (t == null)
                {
                    throw new Exception("Type '" + item.StateBehaviorClass + "' not found.");
                }
                var isImplementing = t.GetInterfaces().Contains(typeof(IStateBehavior<FlowState>));
                if (!isImplementing)
                {
                    throw new Exception("Type '" + item.StateBehaviorClass + "' is not IStateBehavior<" + typeof(FlowState).Name + ">.");
                }
                state.BehaviorType = t;

                states.Add(state);
            }

            return states;
        }

        private static List<StateTransition<FlowState, string, LanguageUnderstandingResult>> GetTransitions(List<FlowState> flowStates, LuisConfiguration configuration)
        {
            var transitions = new List<StateTransition<FlowState, string, LanguageUnderstandingResult>>();
            foreach (var luisFlowStateTransition in configuration.LuisFlowStateTransitions.List)
            {
                CognitiveServicesTest.LanguageUnderstanding.Conditions.IConditionOperator<LanguageUnderstandingResult> op = null;
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

                var currentState = flowStates.First(x => x.Name == luisFlowStateTransition.CurrentState);
                var nextState = flowStates.First(x => x.Name == luisFlowStateTransition.NextState);

                var transition = new LuisFlowStateTransition<FlowState>(currentState, nextState, luisFlowStateTransition.Intent, luisFlowStateTransition.IsFinalState, op);
                transitions.Add(transition);
            }

            return transitions;
        }

        private static AndCondition<LanguageUnderstandingResult> ProcessAndOperator(AndOperator inputAndOperator)
        {
            var andOperator = new CognitiveServicesTest.LanguageUnderstanding.Conditions.AndCondition<LanguageUnderstandingResult>();
            var operators = ProcessSuboperators(inputAndOperator);
            andOperator.Operators = operators.ToArray();
            return andOperator;
        }

        private static IConditionOperator<LanguageUnderstandingResult> ProcessOrOperator(OrOperator op)
        {
            var orOperator = new CognitiveServicesTest.LanguageUnderstanding.Conditions.OrCondition<LanguageUnderstandingResult>();
            var operators = ProcessSuboperators(op);
            orOperator.Operators = operators.ToArray();
            return orOperator;
        }

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

        private static IConditionOperator<LanguageUnderstandingResult> ProcessIsEntityEqualsOperator(IsEntityEquals op)
        {
            return new CognitiveServicesTest.LanguageUnderstanding.Conditions.IsEntityEquals(op.Name, op.Value);
        }
    }
}