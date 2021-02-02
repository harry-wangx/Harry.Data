using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Harry.Data.Test
{
    public class InMemoryTestHelper : TestHelper
    {
        private InMemoryDatabaseRoot databaseRoot = new InMemoryDatabaseRoot();
        protected InMemoryTestHelper()
        { }

        private static Lazy<InMemoryTestHelper> instance = new Lazy<InMemoryTestHelper>(() =>
        {
            var result = new InMemoryTestHelper();
            return result;
        });

        public static InMemoryTestHelper Instance { get; } = instance.Value;

        public override IServiceCollection AddProviderServices(IServiceCollection services)
        {
            services.AddInMemory(null, "InMemory", databaseRoot);
            services.Configure<RepositoryOptions>(options =>
            {
                options.DbLinks.Add(new DbLink.DbLinkItem() { Name = "test", DbType = "InMemory", ConnectionString = "InMemory" });
            });
            return services;
        }

        public override void ConfigDbContextOptions(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("InMemory", databaseRoot);
        }

    }
}
