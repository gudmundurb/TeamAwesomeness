namespace Sozial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LASTMIGRATION : DbMigration
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
                        createdDate = c.DateTime(nullable: false),
                        commentText = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.commentID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.PostModels", t => t.postID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.postID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
            CreateTable(
                "dbo.FavouriteRelationModels",
                c => new
                    {
                        relID = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        gameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.relID);
            
            CreateTable(
                "dbo.FriendshipModels",
                c => new
                    {
                        friendshipID = c.Int(nullable: false, identity: true),
                        UsernameOne = c.String(),
                        UsernameTwo = c.String(),
                    })
                .PrimaryKey(t => t.friendshipID);
            
            CreateTable(
                "dbo.GameModels",
                c => new
                    {
                        gameID = c.Int(nullable: false, identity: true),
                        nameOfGame = c.String(nullable: false),
                        aboutGame = c.String(nullable: false),
                        gameCompany = c.String(nullable: false),
                        isTopTen = c.Boolean(nullable: false),
                        genre = c.String(),
                        imageUrl = c.String(),
                    })
                .PrimaryKey(t => t.gameID);
            
            CreateTable(
                "dbo.GroupModels",
                c => new
                    {
                        groupID = c.Int(nullable: false, identity: true),
                        createdDate = c.DateTime(nullable: false),
                        likeCount = c.Int(nullable: false),
                        groupName = c.String(nullable: false),
                        groupDescription = c.String(nullable: false),
                        groupPicture = c.String(),
                        groupBanner = c.String(),
                        isRecruiting = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.groupID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        steamId = c.String(),
                        userProfilePic = c.String(),
                        userBannerPic = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        GroupModel_groupID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.GroupModels", t => t.GroupModel_groupID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.GroupModel_groupID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PostModels",
                c => new
                    {
                        postID = c.Int(nullable: false, identity: true),
                        userID = c.String(),
                        text = c.String(nullable: false),
                        createdDate = c.DateTime(nullable: false),
                        imageUrl = c.String(),
                        likeCount = c.Int(nullable: false),
                        userPicture = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                        GroupModel_groupID = c.Int(),
                    })
                .PrimaryKey(t => t.postID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .ForeignKey("dbo.GroupModels", t => t.GroupModel_groupID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1)
                .Index(t => t.GroupModel_groupID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.GroupPostRelationModels",
                c => new
                    {
                        relId = c.Int(nullable: false, identity: true),
                        postId = c.Int(nullable: false),
                        groupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.relId);
            
            CreateTable(
                "dbo.GroupRelationshipModels",
                c => new
                    {
                        GroupRelID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.GroupRelID);
            
            CreateTable(
                "dbo.NewsModels",
                c => new
                    {
                        newsID = c.Int(nullable: false, identity: true),
                        userID = c.String(),
                        title = c.String(nullable: false),
                        text = c.String(nullable: false),
                        createdDate = c.DateTime(nullable: false),
                        imageUrl = c.String(),
                    })
                .PrimaryKey(t => t.newsID);
            
            CreateTable(
                "dbo.ProfilePostRelationModels",
                c => new
                    {
                        relId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        postId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.relId);
            
            CreateTable(
                "dbo.ReviewModels",
                c => new
                    {
                        reviewId = c.Int(nullable: false, identity: true),
                        gameId = c.Int(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        userId = c.String(),
                        text = c.String(nullable: false),
                        likeCount = c.Int(nullable: false),
                        gameRating_ratingId = c.Int(),
                    })
                .PrimaryKey(t => t.reviewId)
                .ForeignKey("dbo.RatingModels", t => t.gameRating_ratingId)
                .Index(t => t.gameRating_ratingId);
            
            CreateTable(
                "dbo.RatingModels",
                c => new
                    {
                        ratingId = c.Int(nullable: false, identity: true),
                        gameID = c.Int(nullable: false),
                        Username = c.String(),
                        rating = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ratingId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReviewModels", "gameRating_ratingId", "dbo.RatingModels");
            DropForeignKey("dbo.PostModels", "GroupModel_groupID", "dbo.GroupModels");
            DropForeignKey("dbo.AspNetUsers", "GroupModel_groupID", "dbo.GroupModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostModels", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentModels", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentModels", "postID", "dbo.PostModels");
            DropForeignKey("dbo.CommentModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReviewModels", new[] { "gameRating_ratingId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.PostModels", new[] { "GroupModel_groupID" });
            DropIndex("dbo.PostModels", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.PostModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "GroupModel_groupID" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CommentModels", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.CommentModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CommentModels", new[] { "postID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RatingModels");
            DropTable("dbo.ReviewModels");
            DropTable("dbo.ProfilePostRelationModels");
            DropTable("dbo.NewsModels");
            DropTable("dbo.GroupRelationshipModels");
            DropTable("dbo.GroupPostRelationModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.PostModels");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.GroupModels");
            DropTable("dbo.GameModels");
            DropTable("dbo.FriendshipModels");
            DropTable("dbo.FavouriteRelationModels");
            DropTable("dbo.CommentModels");
        }
    }
}
