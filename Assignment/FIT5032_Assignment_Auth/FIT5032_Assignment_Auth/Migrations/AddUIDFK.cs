namespace FIT5032_Assignment_Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddUIDFK : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Appointment", "Uid", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }

        
    
}
