namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotendiachi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "hoten", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "diachi", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "diachi");
            DropColumn("dbo.AspNetUsers", "hoten");
        }
    }
}
