using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DSEDFinal.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Hazard> Hazards { get; set; }
         
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>()
                .HasRequired(m => m.Member)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hazard>()
                .HasRequired(h => h.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(a => a.DefaultOrganization)
                .WithMany();

            base.OnModelCreating(modelBuilder);
        }
    }
}