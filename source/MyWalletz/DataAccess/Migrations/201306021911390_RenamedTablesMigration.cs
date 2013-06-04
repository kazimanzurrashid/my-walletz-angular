namespace MyWalletz.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenamedTablesMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Transactions", new[] { "UserId", "AccountId", "CategoryId", "PostedAt", "Amount", "Payee" });
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropIndex("dbo.Categories", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Categories", "UserId", "dbo.Users");
            RenameTable("dbo.Users", "mw_Users");
            RenameTable("dbo.Categories", "mw_Categories");
            RenameTable("dbo.Accounts", "mw_Accounts");
            RenameTable("dbo.Transactions", "mw_Transactions");
            AddForeignKey("dbo.mw_Categories", "UserId", "dbo.mw_Users", "Id");
            AddForeignKey("dbo.mw_Accounts", "UserId", "dbo.mw_Users", "Id");
            AddForeignKey("dbo.mw_Transactions", "AccountId", "dbo.mw_Accounts", "Id");
            AddForeignKey("dbo.mw_Transactions", "CategoryId", "dbo.mw_Categories", "Id");
            AddForeignKey("dbo.mw_Transactions", "UserId", "dbo.mw_Users", "Id");
            CreateIndex("dbo.mw_Users", "Email", true);
            CreateIndex("dbo.mw_Categories", "UserId");
            CreateIndex("dbo.mw_Accounts", "UserId");
            CreateIndex("dbo.mw_Transactions", new[] { "UserId", "AccountId", "CategoryId", "PostedAt", "Amount", "Payee" });
        }

        public override void Down()
        {
            DropIndex("dbo.mw_Transactions", new[] { "UserId", "AccountId", "CategoryId", "PostedAt", "Amount", "Payee" });
            DropIndex("dbo.mw_Accounts", new[] { "UserId" });
            DropIndex("dbo.mw_Categories", new[] { "UserId" });
            DropIndex("dbo.mw_Users", new[] { "Email" });
            DropForeignKey("dbo.mw_Transactions", "CategoryId", "dbo.mw_Categories");
            DropForeignKey("dbo.mw_Transactions", "AccountId", "dbo.mw_Accounts");
            DropForeignKey("dbo.mw_Transactions", "UserId", "dbo.mw_Users");
            DropForeignKey("dbo.mw_Accounts", "UserId", "dbo.mw_Users");
            DropForeignKey("dbo.mw_Categories", "UserId", "dbo.mw_Users");
            RenameTable("dbo.mw_Users", "Users");
            RenameTable("dbo.mw_Categories", "Categories");
            RenameTable("dbo.mw_Accounts", "Accounts");
            RenameTable("dbo.mw_Transactions", "Transactions");
            AddForeignKey("dbo.Categories", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Accounts", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Transactions", "UserId", "dbo.Users", "Id");
            CreateIndex("dbo.Users", "Email", true);
            CreateIndex("dbo.Categories", "UserId");
            CreateIndex("dbo.Accounts", "UserId");
            CreateIndex("dbo.Transactions", new[] { "UserId", "AccountId", "CategoryId", "PostedAt", "Amount", "Payee" });
        }
    }
}