using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using ReportApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ReportApp.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
            [Display(Name = "Imie")]
            public string FirstName { get; set; }
            [Display(Name = "Nazwisko")]
            public string LastName { get; set; }
             [Display(Name = "Numer indeksu")]
            public int NumerIndeksu { get; set; }
            public List<int> SelectedPrzedmiotIDs { get; set; }
            [Display(Name = "Numer grupy")]
            public int Nr_grupy { get; set; }
            [Display(Name = "Numer stanowiska")]
            public int Nr_stanowiska { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("EFDbContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ReportApp.Domain.Entities.Event> Events { get; set; }

        public System.Data.Entity.DbSet<ReportApp.Domain.Entities.Report> Reports { get; set; }

        public System.Data.Entity.DbSet<ReportApp.Domain.Entities.Subjects> Subjects { get; set; }
    }
}