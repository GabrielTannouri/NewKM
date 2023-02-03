using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NewKnowledgeManager.Repository.Context
{
    /// <summary>
    /// Created to run the migrations in the Repository project.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Password=123;Persist Security Info=True;User ID=sa;Initial Catalog=NewKnowledgeManagerDb;Data Source=TANNOURI\\SQLEXPRESS";

            // Creating DbContextOptionsBuilder manually
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer(connectionString);

            // Create the context
            return new ApplicationDbContext(builder.Options);
        }
    }
}
