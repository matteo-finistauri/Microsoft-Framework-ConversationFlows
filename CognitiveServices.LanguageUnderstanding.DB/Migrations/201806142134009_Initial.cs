namespace CognitiveServices.LanguageUnderstanding.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LuisFlowStateTransitions", "ConditionId", "dbo.Conditions");
            DropIndex("dbo.LuisFlowStateTransitions", new[] { "ConditionId" });
            RenameColumn(table: "dbo.LuisFlowStateTransitions", name: "ConditionId", newName: "Condition_ID");
            AlterColumn("dbo.LuisFlowStateTransitions", "Condition_ID", c => c.Int());
            CreateIndex("dbo.LuisFlowStateTransitions", "Condition_ID");
            AddForeignKey("dbo.LuisFlowStateTransitions", "Condition_ID", "dbo.Conditions", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LuisFlowStateTransitions", "Condition_ID", "dbo.Conditions");
            DropIndex("dbo.LuisFlowStateTransitions", new[] { "Condition_ID" });
            AlterColumn("dbo.LuisFlowStateTransitions", "Condition_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.LuisFlowStateTransitions", name: "Condition_ID", newName: "ConditionId");
            CreateIndex("dbo.LuisFlowStateTransitions", "ConditionId");
            AddForeignKey("dbo.LuisFlowStateTransitions", "ConditionId", "dbo.Conditions", "ID", cascadeDelete: true);
        }
    }
}
