using CleanArch.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.IoC
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(this IApplicationBuilder app) {
            using (var serviceScore = app.ApplicationServices.CreateScope()) {
                var serviceDB = serviceScore.ServiceProvider.GetService<ApplicationDbContext>();
                serviceDB?.Database.Migrate();
            }
        }
    }
}