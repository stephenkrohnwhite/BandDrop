namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingsendernametoconversationsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "sender_name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "sender_name");
        }
    }
}
