namespace MyWalletz.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c =>
                new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Payee = c.String(nullable: true, maxLength: 128),
                    Notes = c.String(maxLength: 512),
                    AccountId = c.Int(nullable: false),
                    CategoryId = c.Int(),
                    UserId = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PostedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.UserId);

            CreateIndex(
                "dbo.Transactions",
                new[] { "UserId", "AccountId", "CategoryId", "PostedAt", "Amount", "Payee" });
        }

        public override void Down()
        {
            DropIndex(
                "dbo.Transactions",
                new[] { "UserId", "AccountId", "CategoryId", "PostedAt", "Amount", "Payee" });

            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Transactions", "UserId", "dbo.Users");

            DropTable("dbo.Transactions");
        }
    }
}
