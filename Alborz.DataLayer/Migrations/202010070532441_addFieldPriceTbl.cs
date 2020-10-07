namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldPriceTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceTbl", "ProducId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceTbl", "ProducId");
        }
    }
}
