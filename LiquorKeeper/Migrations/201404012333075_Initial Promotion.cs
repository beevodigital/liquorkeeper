namespace LiquorKeeper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialPromotion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StorePromotions",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        Store_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stores", t => t.Store_ID)
                .Index(t => t.Store_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StorePromotions", "Store_ID", "dbo.Stores");
            DropIndex("dbo.StorePromotions", new[] { "Store_ID" });
            DropTable("dbo.StorePromotions");
        }
    }
}
