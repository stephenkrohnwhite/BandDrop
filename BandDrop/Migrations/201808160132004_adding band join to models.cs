namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingbandjointomodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BandJoins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BandName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BandJoins");
        }
    }
}
