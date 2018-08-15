namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowingfornullfkfrommusiciantoband : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Musicians", "BandId", "dbo.Bands");
            DropIndex("dbo.Musicians", new[] { "BandId" });
            AlterColumn("dbo.Musicians", "BandId", c => c.Int());
            CreateIndex("dbo.Musicians", "BandId");
            AddForeignKey("dbo.Musicians", "BandId", "dbo.Bands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Musicians", "BandId", "dbo.Bands");
            DropIndex("dbo.Musicians", new[] { "BandId" });
            AlterColumn("dbo.Musicians", "BandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Musicians", "BandId");
            AddForeignKey("dbo.Musicians", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
        }
    }
}
