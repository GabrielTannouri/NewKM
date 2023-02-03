
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewKnowledgeManager.Application;
using NewKnowledgeManager.Application.DependencyInjection;
using NewKnowledgeManager.Domain.Interfaces;
using NewKnowledgeManager.Repository.Context;
using NewKnowledgeManager.Repository.DependencyInjection;
using NewKnowledgeManager.Repository.Middleware;
using NewKnowledgeManager.Repository.Utils;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddRepository(config);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.UseTenantIdentifier();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    InitDb.RunMigration(serviceScope.ServiceProvider.GetService<ApplicationDbContext>(), builder.Configuration);
}

app.Run();
