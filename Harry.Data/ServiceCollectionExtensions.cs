using Harry.Data;
using Harry.Data.DbLink;
using Harry.Data.Handler;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEFCoreRepository(this IServiceCollection services, Action<IDataBuilder> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            //注册数据库链接管理工厂
            services.TryAddSingleton<IDbLinkFactory, DbLinkFactory>();
            //
            services.TryAddSingleton<RepositoryFactory>();
            services.TryAddSingleton<IRepositoryFactory>(sp => sp.GetRequiredService<RepositoryFactory>());

            //设置操作时间
            services.TryAddScoped<IDataHandler, OperationTimeHandler>();
            //设置并发标记
            services.TryAddScoped<IDataHandler, ConcurrencyStampHandler>();

            services.TryAddScoped(typeof(CommonDbContext));
            services.TryAddScoped(typeof(CommonDbContext<>));
            services.TryAddScoped(typeof(CommonDbContext<,>));
            services.TryAddScoped(typeof(CommonDbContext<,,>));
            services.TryAddScoped(typeof(CommonDbContext<,,,>));
            services.TryAddScoped(typeof(CommonDbContext<,,,,>));
            services.TryAddScoped(typeof(CommonDbContext<,,,,,>));
            services.TryAddScoped(typeof(CommonDbContext<,,,,,,>));
            services.TryAddScoped(typeof(CommonDbContext<,,,,,,,>));
            services.TryAddScoped(typeof(CommonDbContext<,,,,,,,,>));
            services.TryAddScoped(typeof(CommonDbContext<,,,,,,,,,>));

            configure?.Invoke(new DataBuilder(services));
            return services;
        }

        //public static IServiceCollection AddEFCoreRepository(this IServiceCollection services, Action<RepositoryOptions> entityTypeOptionsAction = null)
        //{
        //    //以下可以注册为Singleton类型
        //    services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbLinkProvider, OptionsDbLinkProvider>());
        //    //注册数据库链接管理工厂
        //    services.TryAddSingleton<IDbLinkFactory, DbLinkFactory>();
        //    //
        //    services.TryAddSingleton<RepositoryFactory>();
        //    services.TryAddSingleton<IRepositoryFactory>(sp => sp.GetRequiredService<RepositoryFactory>());

        //    //设置操作时间
        //    services.TryAddScoped<IDataHandler, OperationTimeHandler>();
        //    //设置并发标记
        //    services.TryAddScoped<IDataHandler, ConcurrencyStampHandler>();

        //    services.TryAddScoped(typeof(CommonDbContext));
        //    services.TryAddScoped(typeof(CommonDbContext<>));
        //    services.TryAddScoped(typeof(CommonDbContext<,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,,,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,,,,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,,,,,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,,,,,,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,,,,,,,>));
        //    services.TryAddScoped(typeof(CommonDbContext<,,,,,,,,,>));

        //    services.AddOptions();
        //    services.Configure<RepositoryOptions>(options =>
        //    {
        //        entityTypeOptionsAction?.Invoke(options);
        //    });
        //    return services;
        //}

        //public static IServiceCollection TryAddDbProvider<TDbProvider>(this IServiceCollection services, string dbType, Func<TDbProvider> createFunc)
        //    where TDbProvider : IDbProvider
        //{
        //    services = services ?? throw new ArgumentNullException(nameof(services));
        //    dbType = dbType ?? throw new ArgumentNullException(nameof(dbType));
        //    createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));

        //    if (services.Where(m => m.ServiceType == typeof(IDbProvider)
        //     && m.ImplementationType == typeof(TDbProvider)
        //    ).Count() > 0)
        //    {

        //    }
        //}
    }
}
