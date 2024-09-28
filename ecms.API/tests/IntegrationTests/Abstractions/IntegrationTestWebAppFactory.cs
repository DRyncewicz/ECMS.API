using ecms.Application.Abstractions.Auth;
using ecms.Infrastructure.Authorization;
using ecms.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;

namespace IntegrationTests.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            var testDbName = $"EcmsDbTests_{Guid.NewGuid()}";
            var testConnectionString = $"Server=(localdb)\\mssqllocaldb;Database={testDbName};Trusted_Connection=True;MultipleActiveResultSets=true";

            var customSettings = new Dictionary<string, string>
        {
            { "ConnectionStrings:Database", testConnectionString }
        };

            config.AddInMemoryCollection(customSettings);
        });

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(services.BuildServiceProvider()
                    .GetRequiredService<IConfiguration>()
                    .GetConnectionString("Database"), conf => conf.UseHierarchyId());
            });

            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            var descriptorCurrentUserService = services.SingleOrDefault(
                d => d.ServiceType == typeof(ICurrentUserService));

            if (descriptorCurrentUserService != null)
            {
                services.Remove(descriptorCurrentUserService);
            }

            var descriptorHttpAccessor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IHttpContextAccessor));

            if (descriptorHttpAccessor != null)
            {
                services.Remove(descriptorHttpAccessor);
            }

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
        };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = principal };
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns(context);
            services.AddSingleton(httpContextAccessorMock.Object);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        });
    }
}