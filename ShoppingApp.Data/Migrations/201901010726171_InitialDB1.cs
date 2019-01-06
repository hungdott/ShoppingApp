namespace ShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "MoreImages", c => c.String(storeType: "xml"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "MoreImages", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
