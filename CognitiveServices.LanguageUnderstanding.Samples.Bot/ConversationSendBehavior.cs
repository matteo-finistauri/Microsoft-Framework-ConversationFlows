using CognitiveServices.LanguageUnderstanding.Attributes;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot
{
    [Requires(new string[] { "message", "outputStrings" })]
    public class ConversationSendBehavior : BaseStateBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationSendBehavior"/> class.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="context">The context.</param>
        public ConversationSendBehavior(FlowState state, Dictionary<string, object> context)
            : base(state, context)
        {
        }

        /// <summary>
        /// Executes the behavior.
        /// </summary>
        public override void ExecuteBehavior()
        {
            var message = GetContextObject<Activity>("message");
            var outputStrings = GetContextObject<Dictionary<string, string>>("outputStrings");
            var client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());
            var reply = message.CreateReply();
            reply.Text = outputStrings[State.Name];
            client.Conversations.ReplyToActivityAsync(reply);
        }
    }
}