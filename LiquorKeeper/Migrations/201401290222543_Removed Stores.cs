namespace LiquorKeeper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedStores : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoreProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Stores", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StoreProducts", "Store_ID", "dbo.Stores");
            DropIndex("dbo.StoreProducts", new[] { "Product_ID" });
            DropIndex("dbo.Stores", new[] { "User_Id" });
            DropIndex("dbo.StoreProducts", new[] { "Store_ID" });
            DropTable("dbo.StoreProducts");
            DropTable("dbo.Stores");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        PrimaryPhone = c.String(),
                        Hours = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StoreProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_ID = c.Int(),
                        Store_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.StoreProducts", "Store_ID");
            CreateIndex("dbo.Stores", "User_Id");
            CreateIndex("dbo.StoreProducts", "Product_ID");
            AddForeignKey("dbo.StoreProducts", "Store_ID", "dbo.Stores", "ID");
            AddForeignKey("dbo.Stores", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.StoreProducts", "Product_ID", "dbo.Products", "ID");
        }
    }
}
