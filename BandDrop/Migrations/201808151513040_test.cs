namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BandViewModels", "Email_1", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BandViewModels", "Email_1", c => c.String());
        }
    }
}
