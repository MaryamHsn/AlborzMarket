namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producttbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductTbl", "ParentId", "dbo.ProductTbl");
            DropIndex("dbo.ProductTbl", new[] { "ParentId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ProductTbl", "ParentId");
            AddForeignKey("dbo.ProductTbl", "ParentId", "dbo.ProductTbl", "Id");
        }
    }
}
