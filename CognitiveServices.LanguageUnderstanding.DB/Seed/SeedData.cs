using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            var isTwoOrOperator = new OrOperator()
            {
                IsEntityEquals = new List<IsEntityEquals>()
            };
            isTwoOrOperator.IsEntityEquals.Add(isNumber2);
            isTwoOrOperator.IsEntityEquals.Add(isNumberTwo);

            #endregion Or operators

            #region Conditions

            var isShelfCondition = context.Conditions.Add(new Condition() { IsEntityEquals = isShelf });
            var isArmchairCondition = context.Conditions.Add(new Condition() { IsEntityEquals = isArmchair });
            var isTwoCondition = context.Conditions.Add(new Condition() { OrOperator = isTwoOrOperator });

            #endregion Conditions

            #region Flow states

            List<LuisFlowState> flowStateWithError = new List<LuisFlowState>();
            List<LuisFlowState> allFlowStates = new List<LuisFlowState>();

            var initialFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                IsInitialState = true,
                Name = "InitialState",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.ConversationSendBehavior, Bot Application"
            });
            flowStateWithError.Add(initialFlowState);
            allFlowStates.Add(initialFlowState);

            var buildingShelfFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "BuildingShelf",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(buildingShelfFlowState);
            allFlowStates.Add(buildingShelfFlowState);

            var twoMetersFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "TwoMeters",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(twoMetersFlowState);
            allFlowStates.Add(twoMetersFlowState);

            var buildingArmchairFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "BuildingArmchair",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(buildingArmchairFlowState);
            allFlowStates.Add(buildingArmchairFlowState);

            var buildingSomethingElseFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "BuildingSomethingElse",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(buildingSomethingElseFlowState);
            allFlowStates.Add(buildingSomethingElseFlowState);

            var completedFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "Completed",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            flowStateWithError.Add(completedFlowState);
            allFlowStates.Add(completedFlowState);

            var errorHandlingFlowState = context.FlowStates.Add(new LuisFlowState()
            {
                Name = "ErrorHandling",
                StateBehaviorClass = "CognitiveServices.LanguageUnderstanding.Samples.Bot.PostToUserBehavior, Bot Application"
            });
            allFlowStates.Add(errorHandlingFlowState);

            #endregion Flow states

            #region Flow state transitions

            List<LuisFlowStateTransition> allTransitions = new List<LuisFlowStateTransition>();

            var flowStateTransition1 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = initialFlowState,
                NextState = buildingShelfFlowState,
                Intent = "Build.Furniture",
                Condition = isShelfCondition
            });
            allTransitions.Add(flowStateTransition1);

            var flowStateTransition2 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = initialFlowState,
                NextState = buildingArmchairFlowState,
                Intent = "Build.Furniture",
                Condition = isArmchairCondition
            });
            allTransitions.Add(flowStateTransition2);

            var flowStateTransition3 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = initialFlowState,
                NextState = buildingSomethingElseFlowState,
                Intent = "Build.Furniture"
            });
            allTransitions.Add(flowStateTransition3);

            var flowStateTransition4 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = buildingShelfFlowState,
                NextState = twoMetersFlowState,
                Intent = "Shelf.Size",
                Condition = isTwoCondition
            });
            allTransitions.Add(flowStateTransition4);

            var flowStateTransition5 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = twoMetersFlowState,
                NextState = completedFlowState,
                Intent = "Acknowledgement"
            });
            allTransitions.Add(flowStateTransition5);

            var flowStateTransition6 = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
            {
                CurrentState = completedFlowState,
                NextState = buildingShelfFlowState,
                Intent = "Build.Furniture",
                Condition = isShelfCondition
            });
            allTransitions.Add(flowStateTransition5);

            foreach (var state in flowStateWithError)
            {
                var transition = context.FlowStateTransitions.Add(new LuisFlowStateTransition()
                {
                    CurrentState = state,
                    NextState = errorHandlingFlowState,
                    Intent = "None",
                });
                allTransitions.Add(transition);
            }

            #endregion Flow state transitions

            #region Luis configuration

            var configuration = context.LuisConfigurations.Add(new LuisConfiguration()
            {
                Description = "LUIS Configuration",
                LuisFlowStates = new List<LuisFlowState>(),
                LuisFlowStateTransitions = new List<LuisFlowStateTransition>()
            });
            allFlowStates.ForEach(x => configuration.LuisFlowStates.Add(x));
            allTransitions.ForEach(x => configuration.LuisFlowStateTransitions.Add(x));

            #endregion Luis configuration
        }
    }
}