namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTbl", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTbl", "Description");
        }
    }
}
