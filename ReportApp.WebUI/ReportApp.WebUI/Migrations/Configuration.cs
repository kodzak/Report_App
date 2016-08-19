    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
using ReportApp.WebUI.Models;
    

    internal sealed class Configuration : DbMigrationsConfiguration<ReportApp.WebUI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ReportApp.WebUI.Models.ApplicationDbContext";
        }

         protected override void Seed(ReportApp.WebUI.Models.ApplicationDbContext context)
        {
            var rs = new RoleStore<IdentityRole>(context);
            var rm = new RoleManager<IdentityRole>(rs);

            rm.Create(new IdentityRole { Name = "Administrator" });
            rm.Create(new IdentityRole { Name = "Student" });
            rm.Create(new IdentityRole { Name = "Prowadz¹cy" });

            var us = new UserStore<ApplicationUser>(context);
            var um = new UserManager<ApplicationUser>(us);

            var user = new ApplicationUser { UserName = "admin@admin.pl", Email = "admin@admin.pl", EmailConfirmed = true };
            um.Create(user, "Admin123#");
            um.AddToRole(user.Id, "Administrator");

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
  
