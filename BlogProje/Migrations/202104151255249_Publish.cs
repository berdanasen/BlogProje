namespace BlogProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Publish : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Publish", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Publish");
        }
    }
}
