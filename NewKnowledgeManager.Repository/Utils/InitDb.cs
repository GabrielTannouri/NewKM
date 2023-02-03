using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewKnowledgeManager.Repository.Context;

namespace NewKnowledgeManager.Repository.Utils
{
    /// <summary>
    /// Running the migration and creating the database, if it has not already been created.
    /// </summary>
    public static class InitDb
    {
        public static void RunMigration(ApplicationDbContext context, IConfiguration configuration)
        {
            bool runMigrations = Convert.ToBoolean(configuration["DbRunMigrations"]);
            if (runMigrations)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
        }
    }
}
