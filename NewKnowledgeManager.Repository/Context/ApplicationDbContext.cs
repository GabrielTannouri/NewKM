using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewKnowledgeManager.Domain.Models;
using NewKnowledgeManager.Repository.Mappings;

namespace NewKnowledgeManager.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
                            ) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Tenant>(new TenantMap().Configure);
        }

    }
}
