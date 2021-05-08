using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestAspNetCore_Infrastructure.Data;
using WebApplicationInfrastructure.Data;

namespace TestAspNetCore_XUnitTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<CustomerContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                //
                services.AddDbContextPool<CustomerContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();
                //WebHookHelper.IsMock = true;

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CustomerContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<DbInitializer>>();

                    //var smsService = scopedServices.GetRequiredService<ISMS>();
                    //var emailService = scopedServices.GetRequiredService<IEmail>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();
                    try
                    {
                        // Seed the database with test data.
                        DbInitializer.Initialize(db, logger);
                    }
                    catch (Exception ex)
                    {
                        logger.LogInformation($"error: {ex.Message}");
                    }
                }
            });
        }
    }
}