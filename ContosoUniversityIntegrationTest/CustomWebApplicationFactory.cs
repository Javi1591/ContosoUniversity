using System.Linq;
using ContosoUniversity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoUniversityIntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Run under "Testing" environment
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove existing DbContextOptions<SchoolContext>
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<SchoolContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            // Register SchoolContext against InMemory database for tests
            services.AddDbContext<SchoolContext>(options =>
                options.UseInMemoryDatabase("ContosoU_TestDb"));

            // Build the provider and ensure database is created
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
            db.Database.EnsureCreated();
        });
    }
}
