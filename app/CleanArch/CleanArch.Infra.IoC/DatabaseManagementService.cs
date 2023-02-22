using CleanArch.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CleanArch.Domain.Interfaces;
using CleanArch.Application.Interfaces;
using CleanArch.Infra.Data.Repositories;
using CleanArch.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CleanArch.Infra.IoC
{
    public static class DatabaseManagementService
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var host = config["DBHOST"] ?? "localhost";
            var port = config["DBPORT"] ?? "3306";
            var password = config["MYSQL_PASSWORD"] ?? config.GetConnectionString("MYSQL_PASSWORD");
            var userid = config["MYSQL_USER"] ?? config.GetConnectionString("MYSQL_USER");
            var productsdb = config["MYSQL_DATABASE"] ?? config.GetConnectionString("MYSQL_DATABASE");

            string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={productsdb}";

            services.AddDbContextPool<ApplicationDbContext>(options =>
                  options.UseMySql(mySqlConnStr,
                      ServerVersion.AutoDetect(mySqlConnStr)));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }

        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using (var serviceScore = app.ApplicationServices.CreateScope())
            {
                var serviceDB = serviceScore.ServiceProvider.GetService<ApplicationDbContext>();
                serviceDB?.Database.Migrate();
            }
        }
    }
}