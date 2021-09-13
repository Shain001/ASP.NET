namespace FIT5032_Assignment_Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class o : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appoitment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppDate = c.DateTime(nullable: false, storeType: "date"),
                        Address = c.String(nullable: false),
                        Postcode = c.Int(nullable: false),
                        State = c.String(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Rate = c.Int(),
                        Uid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t=>t.Id, cascadeDelete:true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appoitment", "TypeId", "dbo.Types");
            DropIndex("dbo.Appoitment", new[] { "TypeId" });
            DropTable("dbo.Types");
            DropTable("dbo.Appoitment");
        }
    }
}
