namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPriceTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceTbl", "ProductDetailId", c => c.Int(nullable: false));
            AddColumn("dbo.PriceTbl", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PriceTbl", "ProductId");
            DropColumn("dbo.PriceTbl", "ProductTitle");
            DropColumn("dbo.PriceTbl", "ValidDateFrom");
            DropColumn("dbo.PriceTbl", "ValidDateTo");
            DropColumn("dbo.PriceTbl", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceTbl", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.PriceTbl", "ValidDateTo", c => c.DateTime());
            AddColumn("dbo.PriceTbl", "ValidDateFrom", c => c.DateTime());
            AddColumn("dbo.PriceTbl", "ProductTitle", c => c.String(maxLength: 250));
            AddColumn("dbo.PriceTbl", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.PriceTbl", "Price");
            DropColumn("dbo.PriceTbl", "ProductDetailId");
        }
    }
}
