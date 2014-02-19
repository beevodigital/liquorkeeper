namespace LiquorKeeper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoreProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.StoreProducts", "Store_ID", "dbo.Stores");
            DropIndex("dbo.StoreProducts", new[] { "Product_ID" });
            DropIndex("dbo.StoreProducts", new[] { "Store_ID" });
            DropTable("dbo.Products");
            DropTable("dbo.StoreProducts");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.StoreProducts", "Store_ID");
            CreateIndex("dbo.StoreProducts", "Product_ID");
            AddForeignKey("dbo.StoreProducts", "Store_ID", "dbo.Stores", "ID");
            AddForeignKey("dbo.StoreProducts", "Product_ID", "dbo.Products", "ID");
        }
    }
}
