namespace MobLib.Payment.Tests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PayU.AdditionalValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Value = c.Decimal(nullable: false, precision: 10, scale: 2),
                        CurrencyId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Plan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PayU.Currency", t => t.CurrencyId)
                .ForeignKey("PayU.Plan", t => t.Plan_Id)
                .Index(t => t.CurrencyId)
                .Index(t => t.Plan_Id);
            
            CreateTable(
                "PayU.Currency",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 255, unicode: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PayU.Country",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 255, unicode: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PayU.CreditCardToken",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Document = c.String(nullable: false, maxLength: 255, unicode: false),
                        CreditCardTypeId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Token = c.String(nullable: false, maxLength: 255, unicode: false),
                        Number = c.String(nullable: false, maxLength: 255, unicode: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Address_Line1 = c.String(nullable: false, maxLength: 255, unicode: false),
                        Address_Line2 = c.String(maxLength: 255, unicode: false),
                        Address_Line3 = c.String(maxLength: 255, unicode: false),
                        Address_PostalCode = c.String(nullable: false, maxLength: 255, unicode: false),
                        Address_City = c.String(nullable: false, maxLength: 255, unicode: false),
                        Address_State = c.String(nullable: false, maxLength: 255, unicode: false),
                        CountryId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PayU.Country", t => t.CountryId)
                .ForeignKey("PayU.CreditCardType", t => t.CreditCardTypeId)
                .ForeignKey("PayU.Customer", t => t.Customer_Id)
                .ForeignKey("PayU.Customer", t => t.CustomerId)
                .Index(t => t.CreditCardTypeId)
                .Index(t => t.CustomerId)
                .Index(t => t.CountryId)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "PayU.CreditCardType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 255, unicode: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PayU.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerPayUId = c.String(nullable: false, maxLength: 255, unicode: false),
                        FullName = c.String(nullable: false, maxLength: 255, unicode: false),
                        EmailAddress = c.String(nullable: false, maxLength: 255, unicode: false),
                        ContactPhone = c.String(nullable: false, maxLength: 255, unicode: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PayU.Subscription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        PlanId = c.Int(nullable: false),
                        CreditCardTokenId = c.Int(nullable: false),
                        SubscriptionPayUId = c.String(nullable: false, maxLength: 255, unicode: false),
                        Quantity = c.Int(nullable: false),
                        Installments = c.Int(nullable: false),
                        TrialDays = c.Int(nullable: false),
                        StartPeriod = c.DateTime(nullable: false),
                        EndPeriod = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PayU.CreditCardToken", t => t.CreditCardTokenId)
                .ForeignKey("PayU.Customer", t => t.CustomerId)
                .ForeignKey("PayU.Plan", t => t.PlanId)
                .Index(t => t.CustomerId)
                .Index(t => t.PlanId)
                .Index(t => t.CreditCardTokenId);
            
            CreateTable(
                "PayU.Plan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanPayUId = c.String(nullable: false, maxLength: 255, unicode: false),
                        AccountId = c.Int(nullable: false),
                        PlanCode = c.String(nullable: false, maxLength: 255, unicode: false),
                        Description = c.String(nullable: false, maxLength: 255, unicode: false),
                        IntervalId = c.Int(nullable: false),
                        IntervalCount = c.Int(nullable: false),
                        MaxPaymentsAllowed = c.Int(nullable: false),
                        MaxPaymentAttempts = c.Int(),
                        PaymentAttemptsDelay = c.Int(),
                        MaxPendingPayments = c.Int(),
                        TrialDays = c.Int(),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PayU.PlanInterval", t => t.IntervalId)
                .Index(t => t.IntervalId);
            
            CreateTable(
                "PayU.PlanInterval",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 255, unicode: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PayU.CreditCardToken", "CustomerId", "PayU.Customer");
            DropForeignKey("PayU.Subscription", "PlanId", "PayU.Plan");
            DropForeignKey("PayU.Plan", "IntervalId", "PayU.PlanInterval");
            DropForeignKey("PayU.AdditionalValue", "Plan_Id", "PayU.Plan");
            DropForeignKey("PayU.Subscription", "CustomerId", "PayU.Customer");
            DropForeignKey("PayU.Subscription", "CreditCardTokenId", "PayU.CreditCardToken");
            DropForeignKey("PayU.CreditCardToken", "Customer_Id", "PayU.Customer");
            DropForeignKey("PayU.CreditCardToken", "CreditCardTypeId", "PayU.CreditCardType");
            DropForeignKey("PayU.CreditCardToken", "CountryId", "PayU.Country");
            DropForeignKey("PayU.AdditionalValue", "CurrencyId", "PayU.Currency");
            DropIndex("PayU.Plan", new[] { "IntervalId" });
            DropIndex("PayU.Subscription", new[] { "CreditCardTokenId" });
            DropIndex("PayU.Subscription", new[] { "PlanId" });
            DropIndex("PayU.Subscription", new[] { "CustomerId" });
            DropIndex("PayU.CreditCardToken", new[] { "Customer_Id" });
            DropIndex("PayU.CreditCardToken", new[] { "CountryId" });
            DropIndex("PayU.CreditCardToken", new[] { "CustomerId" });
            DropIndex("PayU.CreditCardToken", new[] { "CreditCardTypeId" });
            DropIndex("PayU.AdditionalValue", new[] { "Plan_Id" });
            DropIndex("PayU.AdditionalValue", new[] { "CurrencyId" });
            DropTable("PayU.PlanInterval");
            DropTable("PayU.Plan");
            DropTable("PayU.Subscription");
            DropTable("PayU.Customer");
            DropTable("PayU.CreditCardType");
            DropTable("PayU.CreditCardToken");
            DropTable("PayU.Country");
            DropTable("PayU.Currency");
            DropTable("PayU.AdditionalValue");
        }
    }
}
