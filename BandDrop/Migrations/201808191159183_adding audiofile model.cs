namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingaudiofilemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AudioFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FileSize = c.Int(),
                        FilePath = c.String(),
                        BandId = c.Int(nullable: false),
                        BandName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AudioFiles");
        }
    }
}
