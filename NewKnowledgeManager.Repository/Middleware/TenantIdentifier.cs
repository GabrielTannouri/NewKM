using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewKnowledgeManager.Repository.Context;
using System;

namespace NewKnowledgeManager.Repository.Middleware
{
    public class TenantIdentifier
    {
        private readonly RequestDelegate _next;

        public TenantIdentifier(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ApplicationDbContext dbContext)
        {
            var tenantGuid = httpContext.Request.Headers["X-Tenant-Guid"].FirstOrDefault();

            if (tenantGuid is not null)
            {
                var tenant = await dbContext.Tenants.FirstOrDefaultAsync(t => t.Id.ToString() == tenantGuid);
                httpContext.Items["Tenant"] = tenant;
            }

            await _next.Invoke(httpContext);
        }
    }


    public static class TenantIdentifierExtension
    {
        public static IApplicationBuilder UseTenantIdentifier(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantIdentifier>();
            return app;
        }
    }
}
