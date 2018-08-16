namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droppinguseremailfrbandviewmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BandViewModels", "Email_1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BandViewModels", "Email_1", c => c.String(nullable: false));
        }
    }
}
