namespace CognitiveServices.LanguageUnderstanding.DB.Migrations
{
    using CognitiveServices.LanguageUnderstanding.DB.Seed;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CognitiveServices.LanguageUnderstanding.DB.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CognitiveServices.LanguageUnderstanding.DB.ApplicationContext context)
        {
            SeedData.Seed(context);
        }
    }
}