namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_message_markasread : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "MarkAsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "MarkAsRead");
        }
    }
}
