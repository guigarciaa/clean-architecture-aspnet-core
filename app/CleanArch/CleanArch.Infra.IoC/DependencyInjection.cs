using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var host = config["DBHOST"] ?? "localhost";
            var port = config["DBPORT"] ?? "3306";
            var password = config["MYSQL_PASSWORD"] ?? config.GetConnectionString("MYSQL_PASSWORD");
            var userid = config["MYSQL_USER"] ?? config.GetConnectionString("MYSQL_USER");
            var productsdb = config["MYSQL_DATABASE"] ?? config.GetConnectionString("MYSQL_DATABASE");

            string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={productsdb}";

            //     services.AddDbContext<ApplicationDbContext>(options =>
            // options.UseSqlite(mySqlConnStr, b => b.MigrationsAssembly("CleanArch.Infra.Data")));

            services.AddDbContextPool<ApplicationDbContext>(options =>
                  options.UseMySql(mySqlConnStr,
                      ServerVersion.AutoDetect(mySqlConnStr)));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}