using Harry.Data.DbLink;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Harry.Data
{
    public static class DataBuilderExtensions
    {
        public static IDataBuilder Configure(this IDataBuilder builder, Action<RepositoryOptions> optionsAction)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

            builder.Services.Configure<RepositoryOptions>(options =>
            {
                optionsAction?.Invoke(options);
            });
            return builder;
        }

        public static IDataBuilder AddDbLink(this IDataBuilder builder, IDbLinkProvider provider)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (provider == null) throw new ArgumentNullException(nameof(provider));


            builder.Services.AddSingleton<IDbLinkProvider>(provider);
            return builder;
        }

        public static IDataBuilder AddDbLink(this IDataBuilder builder, Func<IServiceProvider, IDbLinkProvider> func)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (func == null) throw new ArgumentNullException(nameof(func));


            builder.Services.AddSingleton<IDbLinkProvider>(func);
            return builder;
        }


        public static IDataBuilder AddDbLink(this IDataBuilder builder, string name, string dbType, string connectionString, string description = null)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(dbType)) throw new ArgumentNullException(nameof(dbType));
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));


            return AddDbLink(builder, new CommonDbLinkProvider(name, dbType, connectionString, description));
        }
    }
}
