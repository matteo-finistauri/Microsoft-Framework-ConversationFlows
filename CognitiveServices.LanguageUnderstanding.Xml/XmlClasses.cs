using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CognitiveServices.LanguageUnderstanding.Xml
{
    [Serializable]
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

    [Serializable]
    [XmlRoot(ElementName = "luisFlowStates")]
    public class LuisFlowStates
    {
        [XmlElement(ElementName = "luisFlowState")]
        public List<LuisFlowState> List { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "isEntityEquals")]
    public class IsEntityEquals
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [Serializable]
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

    [Serializable]
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

    [Serializable]
    public class CombinatorialOperator
    {
        [XmlElement(ElementName = "isEntityEquals")]
        public List<IsEntityEquals> IsEntityEquals { get; set; }

        [XmlElement(ElementName = "orOperator")]
        public List<OrOperator> OrOperators { get; set; }

        [XmlElement(ElementName = "andOperator")]
        public List<AndOperator> AndOperators { get; set; }
    }

    [Serializable]
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

    [Serializable]
    [XmlRoot(ElementName = "luisConfiguration")]
    public class LuisConfiguration
    {
        [XmlElement(ElementName = "luisFlowStates")]
        public LuisFlowStates LuisFlowStates { get; set; }

        [XmlElement(ElementName = "luisFlowStateTransitions")]
        public LuisFlowStateTransitions LuisFlowStateTransitions { get; set; }
    }
}