namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admins", "RoleId", "dbo.Roles");
            DropIndex("dbo.Admins", new[] { "RoleId" });
            AlterColumn("dbo.Admins", "RoleId", c => c.Int());
            CreateIndex("dbo.Admins", "RoleId");
            AddForeignKey("dbo.Admins", "RoleId", "dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "RoleId", "dbo.Roles");
            DropIndex("dbo.Admins", new[] { "RoleId" });
            AlterColumn("dbo.Admins", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Admins", "RoleId");
            AddForeignKey("dbo.Admins", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}
