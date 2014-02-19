namespace LiquorKeeper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StoreProducts",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_ID = c.Guid(),
                        Store_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.Stores", t => t.Store_ID)
                .Index(t => t.Product_ID)
                .Index(t => t.Store_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreProducts", "Store_ID", "dbo.Stores");
            DropForeignKey("dbo.StoreProducts", "Product_ID", "dbo.Products");
            DropIndex("dbo.StoreProducts", new[] { "Store_ID" });
            DropIndex("dbo.StoreProducts", new[] { "Product_ID" });
            DropTable("dbo.StoreProducts");
            DropTable("dbo.Products");
        }
    }
}
