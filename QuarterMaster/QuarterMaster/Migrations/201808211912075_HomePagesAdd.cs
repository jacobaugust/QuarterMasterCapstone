namespace QuarterMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HomePagesAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomePages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Announcement = c.String(),
                        SliderPic1 = c.String(),
                        SliderPic2 = c.String(),
                        SliderPic3 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HomePages");
        }
    }
}
