namespace BlogProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DATETIMEREQUIREDREMOVED : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "CreationTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "CreationTime", c => c.DateTime(nullable: false));
        }
    }
}
