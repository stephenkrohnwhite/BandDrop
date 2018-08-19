namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropingaudiofilesize : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AudioFiles", "FileSize");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AudioFiles", "FileSize", c => c.Int());
        }
    }
}
