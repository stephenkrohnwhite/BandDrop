namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droppingfkdatetime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Musicians", "created_at");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Musicians", "created_at", c => c.DateTime(nullable: false));
        }
    }
}
