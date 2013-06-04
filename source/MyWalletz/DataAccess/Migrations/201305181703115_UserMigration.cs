namespace MyWalletz.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserMigration : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(nullable: false, maxLength: 128)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, true);
        }

        public override void Down()
        {
            this.DropIndex("dbo.Users", new[] { "Email" });
            this.DropTable("dbo.Users");
        }
    }
}