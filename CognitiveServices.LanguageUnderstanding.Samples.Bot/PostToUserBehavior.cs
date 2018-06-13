using CognitiveServices.LanguageUnderstanding.Attributes;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot
{
    [Requires(new string[] { "context", "outputStrings" })]
    public class PostToUserBehavior : BaseStateBehavior
    {
        public PostToUserBehavior(FlowState state, Dictionary<string, object> context)
            : base(state, context)
        {
        }

        /// <summary>
        /// Executes the behavior.
        /// </summary>
        public override void ExecuteBehavior()
        {
            var dialogContext = this.GetContextObject<IDialogContext>("context");
            var outputStrings = this.GetContextObject<Dictionary<string, string>>("outputStrings");
            dialogContext.PostAsync(outputStrings[State.Name]);
        }
    }
}