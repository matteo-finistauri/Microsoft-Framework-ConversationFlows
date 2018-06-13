using Autofac;
using CognitiveServices.LanguageUnderstanding.Bot;
using CognitiveServices.LanguageUnderstanding.Bot.Dialogs;
using CognitiveServices.LanguageUnderstanding.Samples.Bot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServices.LanguageUnderstanding.Samples.Bot
{
    public class BotSampleModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<StateProvider>()
                   .Keyed<ILuisCommunicationManagerProvider>(FiberModule.Key_DoNotSerialize)
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterType<RootDialog>().As<IDialog<object>>().InstancePerDependency();
        }
    }
}