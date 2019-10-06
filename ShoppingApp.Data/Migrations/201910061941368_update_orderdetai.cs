namespace ShoppingApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_orderdetai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductName", c => c.String());
            AddColumn("dbo.OrderDetails", "ProductImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProductImg");
            DropColumn("dbo.OrderDetails", "ProductName");
        }
    }
}
