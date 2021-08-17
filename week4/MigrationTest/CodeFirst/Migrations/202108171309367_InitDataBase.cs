namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Student_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_id)
                .Index(t => t.Student_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Units", "Student_id", "dbo.Students");
            DropIndex("dbo.Units", new[] { "Student_id" });
            DropTable("dbo.Units");
            DropTable("dbo.Students");
        }
    }
}
