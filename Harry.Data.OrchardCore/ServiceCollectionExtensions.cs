using Harry.Data;
using Harry.Data.DbLink;
using Harry.Data.OrchardCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IDataBuilder AddSystemDbLink(this IDataBuilder builder, Action<SystemDbLinkOptions> optionsBuilder = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));


            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbLinkProvider, SystemDbLinkProvider>());

            if (optionsBuilder != null)
            {
                builder.Services.Configure(optionsBuilder);
            }

            return builder;
        }
    }
}
