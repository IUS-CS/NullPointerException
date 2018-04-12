namespace Chronos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserAccessAndRefreshToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccessToken", c => c.String());
            AddColumn("dbo.AspNetUsers", "RefreshToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RefreshToken");
            DropColumn("dbo.AspNetUsers", "AccessToken");
        }
    }
}
