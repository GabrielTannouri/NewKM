using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewKnowledgeManager.Domain.Interfaces;

namespace NewKnowledgeManager.Application.DependencyInjection
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserApplication, UserApplication>();

            return services;
        }
    }
}
