using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewKnowledgeManager.Domain.Models;
using NewKnowledgeManager.Repository.Mappings;

namespace NewKnowledgeManager.Repository.Context
{
    public class CustomDbContext : DbContext
    {
        private readonly Tenant _tenant;
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public CustomDbContext(DbContextOptions<CustomDbContext> options,
                               IHttpContextAccessor httpContextAccessor,
                               IConfiguration config) 
                               : base(options)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                _tenant = (Tenant)httpContextAccessor.HttpContext.Items["Tenant"];
                _config = config;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_tenant is null ? _config.GetConnectionString("DefaultConnection") : _tenant.ConnectionStringDb);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Tenant>(new TenantMap().Configure);
        }
    }
}
