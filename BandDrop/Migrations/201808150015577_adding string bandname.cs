namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingstringbandname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musicians", "BandName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musicians", "BandName");
        }
    }
}
