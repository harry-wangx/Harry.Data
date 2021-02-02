using Harry.Data.Samples.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Test
{
    public static class Extensions
    {
        public static IRepository CreateRepository<TEntity>(this IServiceProvider serviceProvider, string name = null)
            where TEntity : class, new()
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            return serviceProvider.GetRequiredService<IRepositoryFactory>().CreateRepository<TEntity>(serviceProvider,name ?? "test");
        }
    }
}
