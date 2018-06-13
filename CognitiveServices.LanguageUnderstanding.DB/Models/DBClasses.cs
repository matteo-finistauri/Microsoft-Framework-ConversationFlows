using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServices.LanguageUnderstanding.DB
{
    public class LuisFlowState
    {
        public int ID { get; set; }

        [Required]
        public string StateBehaviorClass { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsInitialState { get; set; }
    }

    public class IsEntityEquals
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }

    public class Condition
    {
        public int ID { get; set; }

        public IsEntityEquals IsEntityEquals { get; set; }

        public OrOperator OrOperator { get; set; }

        public AndOperator AndOperator { get; set; }
    }

    public class LuisFlowStateTransition
    {
        public int ID { get; set; }

        public Condition Condition { get; set; }

        [Required]
        public LuisFlowState CurrentState { get; set; }

        [Required]
        public LuisFlowState NextState { get; set; }

        [Required]
        public string Intent { get; set; }

        public bool IsFinalState { get; set; }
    }

    public class CombinatorialOperator
    {
        public int ID { get; set; }

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
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public DbSet<LuisFlowState> LuisFlowStates { get; set; }

        public DbSet<LuisFlowStateTransition> LuisFlowStateTransitions { get; set; }
    }
}