namespace Alborz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressType = c.String(maxLength: 150),
                        Province = c.Int(),
                        City = c.Int(),
                        PostalCode = c.String(maxLength: 20),
                        FirstNameReciver = c.String(maxLength: 150),
                        LastNameReciver = c.String(maxLength: 150),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 250),
                        Address = c.String(maxLength: 350),
                        CustomerId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CartItemTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        AddToBasketDateTime = c.DateTime(nullable: false),
                        Guid = c.String(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        LineTotal = c.Decimal(nullable: false, precision: 18, scale: 0),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FinalPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        IsCanceled = c.Boolean(),
                        ReserveJsonData = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CartTbl", t => t.CartId)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.CartTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleType = c.Int(),
                        IP = c.String(maxLength: 15),
                        UserId = c.Int(),
                        CustomerUserId = c.Int(nullable: false),
                        CookieToken = c.String(),
                        Guid = c.String(nullable: false),
                        ValidEndDate = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        TotalDiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FinalAmount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        IsCanceled = c.Boolean(),
                        CancelReasonErrorID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ErrorTbl", t => t.CancelReasonErrorID)
                .Index(t => t.CancelReasonErrorID);
            
            CreateTable(
                "dbo.ErrorTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 400),
                        Code = c.String(maxLength: 50),
                        Descrition = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        OrderDetailNumber = c.String(maxLength: 50),
                        Guid = c.String(nullable: false),
                        IsDelivered = c.Boolean(nullable: false),
                        IsShipped = c.Boolean(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        CancelReasonErrorID = c.Int(),
                        OrderStatusId = c.Int(nullable: false),
                        CurrentOrderProcessId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        customrId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InvoiceTbl", t => t.InvoiceId)
                .ForeignKey("dbo.OrderProcessTbl", t => t.CurrentOrderProcessId)
                .ForeignKey("dbo.ErrorTbl", t => t.CancelReasonErrorID)
                .Index(t => t.InvoiceId)
                .Index(t => t.CancelReasonErrorID)
                .Index(t => t.CurrentOrderProcessId);
            
            CreateTable(
                "dbo.InvoiceTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        InvoiceCode = c.String(maxLength: 50),
                        InvoiceDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(),
                        OrderNumber = c.String(maxLength: 50),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        TotalDiscount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FinalPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        IP = c.String(maxLength: 15),
                        CurrentProcessId = c.Int(),
                        PaymentTypeId = c.Int(),
                        IsPayed = c.Boolean(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InvoiceProcessTbl", t => t.CurrentProcessId)
                .ForeignKey("dbo.CartTbl", t => t.CartId)
                .Index(t => t.CartId)
                .Index(t => t.CurrentProcessId);
            
            CreateTable(
                "dbo.InvoiceProcessHistoryTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        InvoiceHistoryStatus = c.Int(),
                        InvoiceProcessId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InvoiceProcessTbl", t => t.InvoiceProcessId)
                .ForeignKey("dbo.InvoiceTbl", t => t.InvoiceId)
                .Index(t => t.InvoiceId)
                .Index(t => t.InvoiceProcessId);
            
            CreateTable(
                "dbo.InvoiceProcessTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Code = c.String(maxLength: 50),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderProcessHistoryTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        OrderProcessId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderProcessTbl", t => t.OrderProcessId)
                .ForeignKey("dbo.OrderTbl", t => t.OrderId)
                .Index(t => t.OrderId)
                .Index(t => t.OrderProcessId);
            
            CreateTable(
                "dbo.OrderProcessTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 150),
                        Code = c.String(maxLength: 50),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Code = c.String(maxLength: 30),
                        priority = c.Int(),
                        ParentCategoryId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryTbl", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "dbo.ProductTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        IsBuyable = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Code = c.String(maxLength: 30),
                        ParentId = c.Int(),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(maxLength: 350),
                        Brand = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTbl", t => t.ParentId)
                .ForeignKey("dbo.CategoryTbl", t => t.CategoryId)
                .Index(t => t.ParentId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PropertyTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 150),
                        productId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTbl", t => t.productId)
                .ForeignKey("dbo.CategoryTbl", t => t.CategoryId)
                .Index(t => t.productId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PropertyValueTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 350),
                        PropertyId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PropertyTbl", t => t.PropertyId)
                .Index(t => t.PropertyId);
            
            CreateTable(
                "dbo.CustomerTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CustomerTypeId = c.Int(),
                        CustomerCode = c.String(maxLength: 50),
                        IsMemberOfNewsLetter = c.Boolean(),
                        LastVisit = c.DateTime(),
                        ReserveJsonData = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerTypeTbl", t => t.CustomerTypeId)
                .Index(t => t.CustomerTypeId);
            
            CreateTable(
                "dbo.CustomerTypeTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerCode = c.String(maxLength: 50),
                        Title = c.String(maxLength: 50),
                        Description = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartItemId = c.Int(),
                        CartId = c.Int(),
                        CategoryId = c.Int(),
                        CustomerTypeId = c.Int(),
                        CustomerId = c.Int(),
                        InvoiceId = c.Int(),
                        ProductId = c.Int(),
                        Title = c.String(maxLength: 250),
                        PriceDiscount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        PerecentDiscount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        StartDateValid = c.DateTime(),
                        EndDateValid = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InternetPaymentGetwayTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankTitle = c.String(maxLength: 150),
                        BankLogo = c.String(),
                        IPGCode = c.String(),
                        IsOnSite = c.Boolean(),
                        ExtraCode = c.String(),
                        IPGDescription = c.String(),
                        BankPublicToken = c.String(),
                        IPGUrl = c.String(),
                        ReserveJsonData = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPGHistoryTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IPGId = c.Int(),
                        ActiveFromDate = c.DateTime(),
                        ActiveToDate = c.DateTime(),
                        LastChangeActiveDate = c.DateTime(),
                        LastChangedUser = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderOperation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 50),
                        IsApproved = c.Boolean(nullable: false),
                        Status = c.String(maxLength: 60),
                        Comment = c.String(),
                        Currency = c.String(maxLength: 50),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 0),
                        IsCancelled = c.Boolean(nullable: false),
                        CancelledDate = c.DateTime(),
                        CancelReason = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        BankCode = c.String(maxLength: 250),
                        IP = c.String(maxLength: 15),
                        PublicToken = c.String(),
                        SecurityToken = c.String(),
                        ResponseShoppingCode = c.Int(nullable: false),
                        ResponseShoppingDescription = c.String(),
                        ReservationCodeBeforePay = c.String(),
                        ReservationCodeBeforePayDate = c.DateTime(nullable: false),
                        SoldProductJson = c.String(),
                        ValidTPay = c.Int(nullable: false),
                        ValidTPaydescription = c.String(),
                        ReturnLink = c.String(),
                        SendLink = c.String(),
                        IssuanceDate = c.DateTime(nullable: false),
                        ReservationCodeForTransaction = c.String(),
                        PaymentStatus = c.String(),
                        SalesReservedVerify = c.Boolean(nullable: false),
                        SendPaymentResultWebservice = c.Boolean(nullable: false),
                        ResponsePayCode = c.Int(nullable: false),
                        ResponsePayDate = c.DateTime(nullable: false),
                        RefrenceId = c.Int(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        WaitSecond = c.Int(nullable: false),
                        FollowUpId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        IsSuccessful = c.Boolean(nullable: false),
                        FailedReason = c.String(),
                        GatewayCode = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductTitle = c.String(maxLength: 250),
                        Quantity = c.Int(),
                        Description = c.String(),
                        Currency = c.String(maxLength: 50),
                        ValidDateFrom = c.DateTime(),
                        ValidDateTo = c.DateTime(),
                        IsVAlid = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurePassword = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.CustomerTbl", "CustomerTypeId", "dbo.CustomerTypeTbl");
            DropForeignKey("dbo.PropertyTbl", "CategoryId", "dbo.CategoryTbl");
            DropForeignKey("dbo.ProductTbl", "CategoryId", "dbo.CategoryTbl");
            DropForeignKey("dbo.PropertyTbl", "productId", "dbo.ProductTbl");
            DropForeignKey("dbo.PropertyValueTbl", "PropertyId", "dbo.PropertyTbl");
            DropForeignKey("dbo.ProductTbl", "ParentId", "dbo.ProductTbl");
            DropForeignKey("dbo.CategoryTbl", "ParentCategoryId", "dbo.CategoryTbl");
            DropForeignKey("dbo.InvoiceTbl", "CartId", "dbo.CartTbl");
            DropForeignKey("dbo.OrderTbl", "CancelReasonErrorID", "dbo.ErrorTbl");
            DropForeignKey("dbo.OrderProcessHistoryTbl", "OrderId", "dbo.OrderTbl");
            DropForeignKey("dbo.OrderTbl", "CurrentOrderProcessId", "dbo.OrderProcessTbl");
            DropForeignKey("dbo.OrderProcessHistoryTbl", "OrderProcessId", "dbo.OrderProcessTbl");
            DropForeignKey("dbo.OrderTbl", "InvoiceId", "dbo.InvoiceTbl");
            DropForeignKey("dbo.InvoiceProcessHistoryTbl", "InvoiceId", "dbo.InvoiceTbl");
            DropForeignKey("dbo.InvoiceTbl", "CurrentProcessId", "dbo.InvoiceProcessTbl");
            DropForeignKey("dbo.InvoiceProcessHistoryTbl", "InvoiceProcessId", "dbo.InvoiceProcessTbl");
            DropForeignKey("dbo.CartTbl", "CancelReasonErrorID", "dbo.ErrorTbl");
            DropForeignKey("dbo.CartItemTbl", "CartId", "dbo.CartTbl");
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.CustomerTbl", new[] { "CustomerTypeId" });
            DropIndex("dbo.PropertyValueTbl", new[] { "PropertyId" });
            DropIndex("dbo.PropertyTbl", new[] { "CategoryId" });
            DropIndex("dbo.PropertyTbl", new[] { "productId" });
            DropIndex("dbo.ProductTbl", new[] { "CategoryId" });
            DropIndex("dbo.ProductTbl", new[] { "ParentId" });
            DropIndex("dbo.CategoryTbl", new[] { "ParentCategoryId" });
            DropIndex("dbo.OrderProcessHistoryTbl", new[] { "OrderProcessId" });
            DropIndex("dbo.OrderProcessHistoryTbl", new[] { "OrderId" });
            DropIndex("dbo.InvoiceProcessHistoryTbl", new[] { "InvoiceProcessId" });
            DropIndex("dbo.InvoiceProcessHistoryTbl", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceTbl", new[] { "CurrentProcessId" });
            DropIndex("dbo.InvoiceTbl", new[] { "CartId" });
            DropIndex("dbo.OrderTbl", new[] { "CurrentOrderProcessId" });
            DropIndex("dbo.OrderTbl", new[] { "CancelReasonErrorID" });
            DropIndex("dbo.OrderTbl", new[] { "InvoiceId" });
            DropIndex("dbo.CartTbl", new[] { "CancelReasonErrorID" });
            DropIndex("dbo.CartItemTbl", new[] { "CartId" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.PriceTbl");
            DropTable("dbo.PaymentTbl");
            DropTable("dbo.OrderOperation");
            DropTable("dbo.IPGHistoryTbl");
            DropTable("dbo.InternetPaymentGetwayTbl");
            DropTable("dbo.Discount");
            DropTable("dbo.CustomerTypeTbl");
            DropTable("dbo.CustomerTbl");
            DropTable("dbo.PropertyValueTbl");
            DropTable("dbo.PropertyTbl");
            DropTable("dbo.ProductTbl");
            DropTable("dbo.CategoryTbl");
            DropTable("dbo.OrderProcessTbl");
            DropTable("dbo.OrderProcessHistoryTbl");
            DropTable("dbo.InvoiceProcessTbl");
            DropTable("dbo.InvoiceProcessHistoryTbl");
            DropTable("dbo.InvoiceTbl");
            DropTable("dbo.OrderTbl");
            DropTable("dbo.ErrorTbl");
            DropTable("dbo.CartTbl");
            DropTable("dbo.CartItemTbl");
            DropTable("dbo.AddressTbl");
        }
    }
}
