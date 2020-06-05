namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryDTOes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryTbl", "ParentCategoryId", "dbo.CategoryTbl");
            DropIndex("dbo.CategoryTbl", new[] { "ParentCategoryId" });
            DropTable("dbo.CategoryDTOes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        StartDate = c.DateTime(),
                        StartDateString = c.String(),
                        EndDate = c.DateTime(),
                        EndDateString = c.String(),
                        Code = c.String(),
                        priority = c.Int(),
                        ParentCategoryId = c.Int(),
                        Description = c.String(),
                        sortOrder = c.String(),
                        currentFilter = c.String(),
                        searchString = c.String(),
                        page = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CategoryTbl", "ParentCategoryId");
            AddForeignKey("dbo.CategoryTbl", "ParentCategoryId", "dbo.CategoryTbl", "Id");
        }
    }
}
