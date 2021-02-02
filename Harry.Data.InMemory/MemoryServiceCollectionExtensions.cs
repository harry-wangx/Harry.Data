using Harry.Data;
using Harry.Data.InMemory;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MemoryServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemory(this IServiceCollection services, Action<InMemoryDbContextOptionsBuilder> optionsAction = null, string dbType = null, InMemoryDatabaseRoot databaseRoot = null)
        {
            dbType = dbType ?? "InMemory";
            services.AddEntityFrameworkInMemoryDatabase();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbProvider>(new MemoryDbProvider(optionsAction, dbType, databaseRoot)));
            return services;
        }
    }
}
