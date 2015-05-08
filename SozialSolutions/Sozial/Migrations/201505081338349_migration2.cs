namespace Sozial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostModels", "text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostModels", "text");
        }
    }
}
