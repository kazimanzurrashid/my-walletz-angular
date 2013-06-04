namespace MyWalletz.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AccountMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 128),
                    Notes = c.String(maxLength: 512),
                    Type = c.Int(nullable: false),
                    Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Currency = c.String(nullable: false, maxLength: 8),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}