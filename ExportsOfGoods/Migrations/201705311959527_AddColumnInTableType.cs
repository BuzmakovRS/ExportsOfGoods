namespace ExportsOfGoods.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnInTableType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfInspecions", "Time", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOfInspecions", "Time");
        }
    }
}
