namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class esitaddFieldPriceTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceTbl", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.PriceTbl", "ProducId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceTbl", "ProducId", c => c.Int(nullable: false));
            DropColumn("dbo.PriceTbl", "ProductId");
        }
    }
}
