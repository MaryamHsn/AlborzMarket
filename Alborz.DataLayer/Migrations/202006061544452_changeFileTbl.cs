namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFileTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDetailTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColorId = c.Int(),
                        ProductId = c.Int(),
                        Quantity = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTbl", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ColorTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdFile = c.Guid(nullable: false),
                        Title = c.String(),
                        Url = c.String(),
                        Subject = c.String(),
                        Size = c.Int(nullable: false),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        FileTypeEnum = c.Int(nullable: false),
                        EntityEnumId = c.Int(nullable: false),
                        EntityKeyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductTbl", "Color", c => c.String());
            AddColumn("dbo.PropertyTbl", "Value", c => c.String());
            DropTable("dbo.ImagTbl");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImagTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        Subject = c.String(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        CategoryId = c.Int(),
                        ProductId = c.Int(),
                        PostId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ProductDetailTbl", "ProductId", "dbo.ProductTbl");
            DropIndex("dbo.ProductDetailTbl", new[] { "ProductId" });
            DropColumn("dbo.PropertyTbl", "Value");
            DropColumn("dbo.ProductTbl", "Color");
            DropTable("dbo.FileTbl");
            DropTable("dbo.ColorTbl");
            DropTable("dbo.ProductDetailTbl");
        }
    }
}
