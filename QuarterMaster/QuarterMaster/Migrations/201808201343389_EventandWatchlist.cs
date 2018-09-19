namespace QuarterMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventandWatchlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Message = c.String(),
                        RecipientEmail = c.String(),
                        UserEmail = c.String(),
                        SenderEmail = c.String(),
                        SenderPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ticker = c.String(),
                        Subject = c.String(),
                        Start = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JuniorUserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserRoles = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockBasicId = c.Int(nullable: false),
                        StockIncomeStatementId = c.Int(nullable: false),
                        StockBalanceSheetId = c.Int(nullable: false),
                        StockMetricsId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        JuniorUserAccount_Id = c.Int(),
                        SeniorUserAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.StockBalanceSheets", t => t.StockBalanceSheetId, cascadeDelete: true)
                .ForeignKey("dbo.StockBasics", t => t.StockBasicId, cascadeDelete: true)
                .ForeignKey("dbo.StockIncomeStatements", t => t.StockIncomeStatementId, cascadeDelete: true)
                .ForeignKey("dbo.StockMetrics", t => t.StockMetricsId, cascadeDelete: true)
                .ForeignKey("dbo.JuniorUserAccounts", t => t.JuniorUserAccount_Id)
                .ForeignKey("dbo.SeniorUserAccounts", t => t.SeniorUserAccount_Id)
                .Index(t => t.StockBasicId)
                .Index(t => t.StockIncomeStatementId)
                .Index(t => t.StockBalanceSheetId)
                .Index(t => t.StockMetricsId)
                .Index(t => t.EventId)
                .Index(t => t.JuniorUserAccount_Id)
                .Index(t => t.SeniorUserAccount_Id);
            
            CreateTable(
                "dbo.StockBalanceSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ticker = c.String(),
                        totalassets = c.Decimal(precision: 18, scale: 2),
                        totalliabilities = c.Decimal(precision: 18, scale: 2),
                        totalequity = c.Decimal(precision: 18, scale: 2),
                        retainedearnings = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockBasics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ticker = c.String(),
                        name = c.String(),
                        stock_exchange = c.String(),
                        sector = c.String(),
                        company_url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockIncomeStatements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ticker = c.String(),
                        netincome = c.Decimal(precision: 18, scale: 2),
                        totalrevenue = c.Decimal(precision: 18, scale: 2),
                        basiceps = c.Decimal(precision: 18, scale: 2),
                        totalgrossprofit = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockMetrics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ticker = c.String(),
                        profitmargin = c.Decimal(precision: 18, scale: 2),
                        grossmargin = c.Decimal(precision: 18, scale: 2),
                        currentratio = c.Single(),
                        dividendyield = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SeniorUserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.WatchLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WatchLists", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.WatchLists", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stocks", "SeniorUserAccount_Id", "dbo.SeniorUserAccounts");
            DropForeignKey("dbo.SeniorUserAccounts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Stocks", "JuniorUserAccount_Id", "dbo.JuniorUserAccounts");
            DropForeignKey("dbo.Stocks", "StockMetricsId", "dbo.StockMetrics");
            DropForeignKey("dbo.Stocks", "StockIncomeStatementId", "dbo.StockIncomeStatements");
            DropForeignKey("dbo.Stocks", "StockBasicId", "dbo.StockBasics");
            DropForeignKey("dbo.Stocks", "StockBalanceSheetId", "dbo.StockBalanceSheets");
            DropForeignKey("dbo.Stocks", "EventId", "dbo.Events");
            DropForeignKey("dbo.JuniorUserAccounts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WatchLists", new[] { "ApplicationUserId" });
            DropIndex("dbo.WatchLists", new[] { "StockId" });
            DropIndex("dbo.SeniorUserAccounts", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Stocks", new[] { "SeniorUserAccount_Id" });
            DropIndex("dbo.Stocks", new[] { "JuniorUserAccount_Id" });
            DropIndex("dbo.Stocks", new[] { "EventId" });
            DropIndex("dbo.Stocks", new[] { "StockMetricsId" });
            DropIndex("dbo.Stocks", new[] { "StockBalanceSheetId" });
            DropIndex("dbo.Stocks", new[] { "StockIncomeStatementId" });
            DropIndex("dbo.Stocks", new[] { "StockBasicId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.JuniorUserAccounts", new[] { "ApplicationUserId" });
            DropTable("dbo.WatchLists");
            DropTable("dbo.SeniorUserAccounts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.StockMetrics");
            DropTable("dbo.StockIncomeStatements");
            DropTable("dbo.StockBasics");
            DropTable("dbo.StockBalanceSheets");
            DropTable("dbo.Stocks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.JuniorUserAccounts");
            DropTable("dbo.Events");
            DropTable("dbo.Emails");
        }
    }
}
