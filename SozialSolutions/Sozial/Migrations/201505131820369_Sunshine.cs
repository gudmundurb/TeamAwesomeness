namespace Sozial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sunshine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostModels", "userPicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostModels", "userPicture");
        }
    }
}
