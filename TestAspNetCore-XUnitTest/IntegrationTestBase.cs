using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using TestAspNetCore;
using TestAspNetCore_Infrastructure.Data;
using Xunit;

namespace TestAspNetCore_XUnitTest
{
    public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly CustomWebApplicationFactory<Startup> _factory;
        protected HttpClient _client;
        protected readonly CustomerContext _customerContext;

        public IntegrationTestBase(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _customerContext = CreateDbContext();
        }

        protected TResult Create<TResult>()
        {
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture.Create<TResult>();
        }
        
        protected virtual async Task UsingDbContextAsync(Func<CustomerContext, Task> action)
        {
            using (var context = CreateDbContext())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        private CustomerContext CreateDbContext()
        {
            var context = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<CustomerContext>();
            return context;
        }

    }
}