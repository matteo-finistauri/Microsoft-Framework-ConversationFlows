using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServices.LanguageUnderstanding.DB.Seed
{
    public class SeedData
    {
        public static void Seed(ApplicationContext context)
        {
            #region Is Entity Equals

            var isShelf = context.IsEntityEquals.Add(new IsEntityEquals() { Name = "FurnitureType", Value = "shelf" });
            var isArmchair = context.IsEntityEquals.Add(new IsEntityEquals() { Name = "FurnitureType", Value = "armchair" });
            var isNumberTwo = context.IsEntityEquals.Add(new IsEntityEquals() { Name = "builtin.number", Value = "Two" });
            var isNumber2 = context.IsEntityEquals.Add(new IsEntityEquals() { Name = "builtin.number", Value = "2" });

            #endregion Is Entity Equals

            #region Or operators

            var isTwoOrOperator = new OrOperator();
            isTwoOrOperator.IsEntityEquals.AddRange(new IsEntityEquals[] { isNumber2, isNumberTwo });

            #endregion Or operators

            #region Conditions

            var isShelfCondition = context.Conditions.Add(new Condition() { IsEntityEquals = isShelf });
            var isArmchairCondition = context.Conditions.Add(new Condition() { IsEntityEquals = isArmchair });
            var isTwoCondition = context.Conditions.Add(new Condition() { OrOperator = isTwoOrOperator });

            #endregion Conditions

            #region Flow states

            List<LuisFlowState> flowStateWithError = new List<LuisFlowState>();

            var initialFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                IsInitialState = true,
                Name = "Initial State",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.ConversationSendBehavior, Bot Application"
            });
            flowStateWithError.Add(initialFlowState);

            var buildingShelfFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "Building Shelf",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(buildingShelfFlowState);

            var twoMetersFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "Two Meters",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(twoMetersFlowState);

            var buildingArmchairFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "Building Armchair",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(buildingArmchairFlowState);

            var buildingSomethingElseFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "Building something else",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(buildingSomethingElseFlowState);

            var completedFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "Completed",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(completedFlowState);

            var errorHandlingFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "Error Handling",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });

            #endregion Flow states

            #region Flow state transitions

            var flowStateTransition1 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = initialFlowState,
                NextState = buildingShelfFlowState,
                Intent = "Build.Furniture",
                Condition = isShelfCondition
            });

            var flowStateTransition2 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = initialFlowState,
                NextState = buildingArmchairFlowState,
                Intent = "Build.Furniture",
                Condition = isArmchairCondition
            });

            var flowStateTransition3 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = initialFlowState,
                NextState = buildingSomethingElseFlowState,
                Intent = "Build.Furniture"
            });

            var flowStateTransition4 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = buildingShelfFlowState,
                NextState = twoMetersFlowState,
                Intent = "Shelf.Size",
                Condition = isTwoCondition
            });

            var flowStateTransition5 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = twoMetersFlowState,
                NextState = completedFlowState,
                Intent = "Acknowledgement"
            });

            foreach (var state in flowStateWithError)
            {
                context.FlowStateTransitions.Add(new LuisFlowStateTransition()
                {
                    CurrentState = state,
                    NextState = errorHandlingFlowState,
                    Intent = "None",
                });
            }

            #endregion Flow state transitions
        }
    }
}