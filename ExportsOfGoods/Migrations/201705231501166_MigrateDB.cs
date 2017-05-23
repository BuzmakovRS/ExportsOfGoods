namespace ExportsOfGoods.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Partis", "InspectionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partis", "InspectionDate", c => c.DateTime());
        }
    }
}
