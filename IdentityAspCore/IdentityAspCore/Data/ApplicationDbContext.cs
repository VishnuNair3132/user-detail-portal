using IdentityAspCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAspCore.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityRole>().HasData(
            //new IdentityRole { Name ="Admin", NormalizedName="Admin", ConcurrencyStamp="1"},
            //new IdentityRole { Name = "User", NormalizedName = "User", ConcurrencyStamp = "2" },
            //new IdentityRole { Name = "Guest", NormalizedName = "Guest", ConcurrencyStamp = "3" });

            base.OnModelCreating(modelBuilder);

            seedRoles(modelBuilder);

        }

        public  static void seedRoles(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = "1" },
            new IdentityRole { Name = "User", NormalizedName = "User", ConcurrencyStamp = "2" },
            new IdentityRole { Name = "Guest", NormalizedName = "Guest", ConcurrencyStamp = "3" });
            
        }

    }
}
