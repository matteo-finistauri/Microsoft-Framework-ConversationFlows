﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using CognitiveServices.LanguageUnderstanding.Bot.Dialogs;
using CognitiveServices.LanguageUnderstanding.Samples.Bot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// The scope
        /// </summary>
        private readonly ILifetimeScope scope;

        /// <summary>
        /// The provider
        /// </summary>
        private readonly ILuisCommunicationManagerProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public MessagesController(ILuisCommunicationManagerProvider provider)
        {
            this.scope = WebApiApplication.FindContainer();
            this.provider = provider;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                var builder = new ContainerBuilder();
                var container = builder.Build();
                await Conversation.SendAsync(activity, () =>
                {
                    using (var scope = DialogModule.BeginLifetimeScope(this.scope, activity))
                    {
                        return scope.Resolve<IDialog<object>>();
                    }
                });
            }
            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        /// <summary>
        /// Handles the system message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
                if (message.MembersAdded != null && message.MembersAdded.Any())
                {
                    foreach (var newMember in message.MembersAdded)
                    {
                        if (newMember.Id != message.Recipient.Id)
                        {
                            var client = this.provider.Instance;
                            client.SetContext("message", message);
                            client.Start();
                        }
                    }
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}