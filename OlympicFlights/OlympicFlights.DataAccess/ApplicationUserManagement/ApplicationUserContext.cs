using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OlympicFlights.DataAccess.ApplicationUserManagement
{
    public class ApplicationUserContext : IdentityDbContext
    {
        public ApplicationUserContext()
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MBVJ3CR\SQLEXPRESS;Initial Catalog=OlympicFlightsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
