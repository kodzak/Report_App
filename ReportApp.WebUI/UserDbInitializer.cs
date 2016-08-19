using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReportApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReportApp.WebUI
{
    public class UserDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var rs = new RoleStore<IdentityRole>(context);
            var rm = new RoleManager<IdentityRole>(rs);

            rm.Create(new IdentityRole { Name = "Administrator" });
            rm.Create(new IdentityRole { Name = "Student" });
            rm.Create(new IdentityRole { Name = "Prowadzący" });

            var us = new UserStore<ApplicationUser>(context);
            var um = new UserManager<ApplicationUser>(us);

            var user = new ApplicationUser { UserName = "admin@admin.pl", Email = "admin@admin.pl", EmailConfirmed = true };
            um.Create(user, "Admin123#");
            um.AddToRole(user.Id, "Administrator");
        }
    }
}