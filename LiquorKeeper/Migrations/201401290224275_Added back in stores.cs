namespace LiquorKeeper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedbackinstores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreProducts",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_ID = c.Int(),
                        Store_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.Stores", t => t.Store_ID)
                .Index(t => t.Product_ID)
                .Index(t => t.Store_ID);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreProducts", "Store_ID", "dbo.Stores");
            DropForeignKey("dbo.Stores", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StoreProducts", "Product_ID", "dbo.Products");
            DropIndex("dbo.StoreProducts", new[] { "Store_ID" });
            DropIndex("dbo.Stores", new[] { "User_Id" });
            DropIndex("dbo.StoreProducts", new[] { "Product_ID" });
            DropTable("dbo.Stores");
            DropTable("dbo.StoreProducts");
        }
    }
}
