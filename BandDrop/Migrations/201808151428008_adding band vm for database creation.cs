namespace BandDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingbandvmfordatabasecreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BandViewModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email_1 = c.String(),
                        Email_2 = c.String(),
                        Email_3 = c.String(),
                        Email_4 = c.String(),
                        Email_5 = c.String(),
                        Email_6 = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BandViewModels");
        }
    }
}
