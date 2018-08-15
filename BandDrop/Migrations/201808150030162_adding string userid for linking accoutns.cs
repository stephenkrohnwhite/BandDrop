namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingstringuseridforlinkingaccoutns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musicians", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musicians", "UserId");
        }
    }
}
