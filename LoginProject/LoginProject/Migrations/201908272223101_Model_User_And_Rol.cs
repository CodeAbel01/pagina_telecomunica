namespace LoginProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Model_User_And_Rol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RolID = c.Int(nullable: false, identity: true),
                        RolName = c.String(),
                    })
                .PrimaryKey(t => t.RolID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RoldID = c.Int(nullable: false),
                        Rol_RolID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.Rol_RolID)
                .Index(t => t.Rol_RolID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "Rol_RolID", "dbo.Roles");
            DropIndex("dbo.Usuarios", new[] { "Rol_RolID" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Roles");
        }
    }
}
