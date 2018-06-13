using Autofac;
using Autofac.Integration.WebApi;
using CognitiveServices.LanguageUnderstanding.Bot;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var config = GlobalConfiguration.Configuration;
            Conversation.UpdateContainer(
                builder =>
                {
                    builder.RegisterModule(new DialogModule());
                    builder.RegisterModule(new BotSampleModule());

                    builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
                    builder.RegisterWebApiFilterProvider(config);
                });
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);
        }

        public static ILifetimeScope FindContainer()
        {
            var config = GlobalConfiguration.Configuration;
            var resolver = (AutofacWebApiDependencyResolver)config.DependencyResolver;
            return resolver.Container;
        }
    }
}