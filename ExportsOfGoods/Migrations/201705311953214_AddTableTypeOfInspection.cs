namespace ExportsOfGoods.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTypeOfInspection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfInspecions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Partis", "TypeOfInspectionId", c => c.Int());
            CreateIndex("dbo.Partis", "TypeOfInspectionId");
            AddForeignKey("dbo.Partis", "TypeOfInspectionId", "dbo.TypeOfInspecions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partis", "TypeOfInspectionId", "dbo.TypeOfInspecions");
            DropIndex("dbo.Partis", new[] { "TypeOfInspectionId" });
            DropColumn("dbo.Partis", "TypeOfInspectionId");
            DropTable("dbo.TypeOfInspecions");
        }
    }
}
