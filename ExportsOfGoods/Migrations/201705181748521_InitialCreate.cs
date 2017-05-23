namespace ExportsOfGoods.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Customs",
                c => new
                    {
                        CustomsId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SenderId = c.Int(),
                        RecipientId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomsId)
                .ForeignKey("dbo.Countries", t => t.RecipientId)
                .ForeignKey("dbo.Countries", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId);
            
            CreateTable(
                "dbo.CustomsQueues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomsId = c.Int(nullable: false),
                        PartiId = c.Int(nullable: false),
                        TimeBegInsp = c.DateTime(nullable: false),
                        TimeEndInsp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customs", t => t.CustomsId, cascadeDelete: true)
                .ForeignKey("dbo.Partis", t => t.PartiId, cascadeDelete: true)
                .Index(t => t.CustomsId)
                .Index(t => t.PartiId);
            
            CreateTable(
                "dbo.Partis",
                c => new
                    {
                        PartiId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        PartiSize = c.Int(nullable: false),
                        InspectionTime = c.DateTime(),
                        InspectionDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartiId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Producer = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomsQueues", "PartiId", "dbo.Partis");
            DropForeignKey("dbo.Partis", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomsQueues", "CustomsId", "dbo.Customs");
            DropForeignKey("dbo.Customs", "SenderId", "dbo.Countries");
            DropForeignKey("dbo.Customs", "RecipientId", "dbo.Countries");
            DropIndex("dbo.Partis", new[] { "ProductId" });
            DropIndex("dbo.CustomsQueues", new[] { "PartiId" });
            DropIndex("dbo.CustomsQueues", new[] { "CustomsId" });
            DropIndex("dbo.Customs", new[] { "RecipientId" });
            DropIndex("dbo.Customs", new[] { "SenderId" });
            DropTable("dbo.Products");
            DropTable("dbo.Partis");
            DropTable("dbo.CustomsQueues");
            DropTable("dbo.Customs");
            DropTable("dbo.Countries");
        }
    }
}
