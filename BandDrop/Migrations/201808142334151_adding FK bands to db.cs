namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingFKbandstodb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BandName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Musicians", "BandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Musicians", "BandId");
            AddForeignKey("dbo.Musicians", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Musicians", "BandId", "dbo.Bands");
            DropIndex("dbo.Musicians", new[] { "BandId" });
            DropColumn("dbo.Musicians", "BandId");
            DropTable("dbo.Bands");
        }
    }
}
