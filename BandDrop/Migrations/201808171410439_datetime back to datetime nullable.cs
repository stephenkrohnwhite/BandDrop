namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimebacktodatetimenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Conversations", "created_at", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Conversations", "created_at", c => c.DateTime(nullable: false));
        }
    }
}
