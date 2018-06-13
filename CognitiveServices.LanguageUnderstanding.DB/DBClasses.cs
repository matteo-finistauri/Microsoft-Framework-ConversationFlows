using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServices.LanguageUnderstanding.DB
{
    public class LuisFlowState
    {
        public string StateBehaviorClass { get; set; }

        public string Name { get; set; }

        public bool IsInitialState { get; set; }
    }

    public class IsEntityEquals
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class Condition
    {
        public IsEntityEquals IsEntityEquals { get; set; }

        public OrOperator OrOperator { get; set; }

        public AndOperator AndOperator { get; set; }
    }

    public class LuisFlowStateTransition
    {
        public Condition Condition { get; set; }

        public string CurrentState { get; set; }

        public string NextState { get; set; }

        public string Intent { get; set; }

        public bool IsFinalState { get; set; }
    }

    public class CombinatorialOperator
    {
        public DbSet<IsEntityEquals> IsEntityEquals { get; set; }

        public DbSet<OrOperator> OrOperators { get; set; }

        public DbSet<AndOperator> AndOperators { get; set; }
    }

    public class OrOperator : CombinatorialOperator
    {
    }

    public class AndOperator : CombinatorialOperator
    {
    }

    public class LuisConfiguration
    {
        public DbSet<LuisFlowState> LuisFlowStates { get; set; }

        public DbSet<LuisFlowStateTransition> LuisFlowStateTransitions { get; set; }
    }
}