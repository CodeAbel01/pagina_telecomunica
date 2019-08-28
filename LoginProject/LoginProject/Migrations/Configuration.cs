namespace LoginProject.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LoginProject.Models.LoginContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LoginContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Roles.AddOrUpdate(
              p => p.RolName,
              new Rol { RolName = "Administrador" },
              new Rol { RolName = "Cliente" }
            );

            context.Usuarios.AddOrUpdate(
                u => u.Nombre,
                new Usuario { Nombre = "Abel", Apellido = "Cabrera", Direccion = "Reu", UserName = "Acabrera", Password = "123", RoldID = 1 }
            );
        }
    }
}
