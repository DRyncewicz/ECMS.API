using Bogus;
using ecms.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        ApplicationDbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        CreateDb();
        Faker = new Faker();
    }

    protected ISender Sender { get; }
    protected ApplicationDbContext ApplicationDbContext { get; }
    protected Faker Faker { get; }

    private void CreateDb()
    {
        ApplicationDbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        ApplicationDbContext.Database.EnsureDeleted();
        _scope.Dispose();
        GC.SuppressFinalize(this);
    }
}