namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_talent_talentrange_update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Talents", "TalentRange", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Talents", "TalentRange", c => c.Int(nullable: false));
        }
    }
}
