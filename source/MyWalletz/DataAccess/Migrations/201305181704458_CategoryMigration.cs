namespace MyWalletz.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CategoryMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 64),
                    Type = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropIndex("dbo.Categories", new[] { "UserId" });
            DropForeignKey("dbo.Categories", "UserId", "dbo.Users");
            DropTable("dbo.Categories");
        }
    }
}