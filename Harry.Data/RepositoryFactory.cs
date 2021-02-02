using Harry.Data.DbLink;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;


namespace Harry.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, IDbProvider> _dbProviders = new Dictionary<string, IDbProvider>();

        public RepositoryFactory(IServiceProvider serviceProvider, IEnumerable<IDbProvider> dbProviders)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            foreach (var item in dbProviders)
            {
                _dbProviders[item.DbType] = item;
            }
        }

        public IRepository CreateRepository(IServiceProvider serviceProvider, IDbLinkItem dbLink, params Type[] entityTypes)
        {
            Check.NotNull(dbLink, nameof(dbLink));

            Check.NotNull(serviceProvider, nameof(serviceProvider));

            //if (!_dbProviders.ContainsKey(dbLink.DbType)) throw new Exception($"未找到名称为:{dbLink.DbType}的数据库配置.");

            //var dbProvider = _dbProviders[dbLink.DbType];

            if (!_dbProviders.TryGetValue(dbLink.DbType, out IDbProvider dbProvider))
            {
                throw new Exception($"未找到名称为:{dbLink.DbType}的数据库配置.");
            }

            //var dbContext = new CommonDbContext(dbProvider, dbLink.ConnectionString, modelBuilder =>
            // {
            //     if (entityTypes != null && entityTypes.Length > 0)
            //     {
            //         foreach (var item in entityTypes)
            //         {
            //             _options.GetModelBuilderAction(item).Invoke(modelBuilder);
            //         }
            //     }
            // });

            var dbType = GetCommonDbContextType(entityTypes);

            if (dbType == null)
            {
                throw new Exception($"未能根据传入的实体类型,找到对应的CommonDbContext.参数entityTypes的数量为:{entityTypes?.Length.ToString()}");
            }

            return new Repository(serviceProvider, () =>
            {
                var dbContext = serviceProvider.GetRequiredService(dbType) as CommonDbContext;

                dbContext?.Init(dbProvider, dbLink.ConnectionString);
                return dbContext;
            }, dbProvider);
        }


        /// <summary>
        /// 获取 CommonDbContext 的类型
        /// </summary>
        /// <param name="entityTypes"></param>
        /// <returns></returns>
        public virtual Type GetCommonDbContextType(Type[] entityTypes)
        {
            Type dbType = null;
            if (entityTypes == null || entityTypes.Length <= 0)
            {
                dbType = typeof(CommonDbContext);
            }
            else
            {
                switch (entityTypes.Length)
                {
                    case 1:
                        dbType = typeof(CommonDbContext<>).MakeGenericType(entityTypes);
                        break;
                    case 2:
                        dbType = typeof(CommonDbContext<,>).MakeGenericType(entityTypes);
                        break;
                    case 3:
                        dbType = typeof(CommonDbContext<,,>).MakeGenericType(entityTypes);
                        break;
                    case 4:
                        dbType = typeof(CommonDbContext<,,,>).MakeGenericType(entityTypes);
                        break;
                    case 5:
                        dbType = typeof(CommonDbContext<,,,,>).MakeGenericType(entityTypes);
                        break;
                    case 6:
                        dbType = typeof(CommonDbContext<,,,,,>).MakeGenericType(entityTypes);
                        break;
                    case 7:
                        dbType = typeof(CommonDbContext<,,,,,,>).MakeGenericType(entityTypes);
                        break;
                    case 8:
                        dbType = typeof(CommonDbContext<,,,,,,,>).MakeGenericType(entityTypes);
                        break;
                    case 9:
                        dbType = typeof(CommonDbContext<,,,,,,,,>).MakeGenericType(entityTypes);
                        break;
                    case 10:
                        dbType = typeof(CommonDbContext<,,,,,,,,,>).MakeGenericType(entityTypes);
                        break;
                }
            }

            return dbType;
        }
    }
}
