using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServices.LanguageUnderstanding.DB
{
    public class LuisFlowState
    {
        [Column(Order = 1)]
        public int ID { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Name { get; set; }

        [Column(Order = 3)]
        public bool IsInitialState { get; set; }

        [Column(Order = 4)]
        [Required]
        public string StateBehaviorClass { get; set; }
    }

    public class IsEntityEquals
    {
        [Column(Order = 1)]
        public int ID { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Name { get; set; }

        [Column(Order = 3)]
        [Required]
        public string Value { get; set; }
    }

    public class Condition
    {
        [Column(Order = 1)]
        public int ID { get; set; }

        [Column(Order = 2)]
        public virtual IsEntityEquals IsEntityEquals { get; set; }

        [Column(Order = 3)]
        public virtual OrOperator OrOperator { get; set; }

        [Column(Order = 4)]
        public virtual AndOperator AndOperator { get; set; }
    }

    public class LuisFlowStateTransition
    {
        [Column(Order = 1)]
        public int ID { get; set; }

        [Column(Order = 2)]
        public virtual Condition Condition { get; set; }

        [Column(Order = 3)]
        public virtual LuisFlowState CurrentState { get; set; }

        [Column(Order = 4)]
        public virtual LuisFlowState NextState { get; set; }

        [Column(Order = 5)]
        [Required]
        public string Intent { get; set; }

        [Column(Order = 6)]
        public bool IsFinalState { get; set; }
    }

    public class CombinatorialOperator
    {
        [Column(Order = 1)]
        public int ID { get; set; }

        [Column(Order = 2)]
        public virtual ICollection<IsEntityEquals> IsEntityEquals { get; set; }

        [Column(Order = 3)]
        public virtual ICollection<OrOperator> OrOperators { get; set; }

        [Column(Order = 4)]
        public virtual ICollection<AndOperator> AndOperators { get; set; }
    }

    public class OrOperator : CombinatorialOperator
    {
    }

    public class AndOperator : CombinatorialOperator
    {
    }

    public class LuisConfiguration
    {
        [Column(Order = 1)]
        public int ID { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Description { get; set; }

        [Column(Order = 3)]
        public virtual ICollection<LuisFlowState> LuisFlowStates { get; set; }

        [Column(Order = 4)]
        public virtual ICollection<LuisFlowStateTransition> LuisFlowStateTransitions { get; set; }
    }
}