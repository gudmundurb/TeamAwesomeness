namespace Sozial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        commentID = c.Int(nullable: false, identity: true),
                        authorID = c.String(),
                        postID = c.Int(nullable: false),
                        showDate = c.DateTime(nullable: false),
                        commentText = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.commentID)
                .ForeignKey("dbo.PostModels", t => t.postID, cascadeDelete: true)
                .Index(t => t.postID);
            
            CreateTable(
                "dbo.PostModels",
                c => new
                    {
                        postID = c.Int(nullable: false, identity: true),
                        userID = c.String(),
                        showDate = c.DateTime(nullable: false),
                        imageFile = c.String(),
                    })
                .PrimaryKey(t => t.postID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentModels", "postID", "dbo.PostModels");
            DropIndex("dbo.CommentModels", new[] { "postID" });
            DropTable("dbo.PostModels");
            DropTable("dbo.CommentModels");
        }
    }
}
