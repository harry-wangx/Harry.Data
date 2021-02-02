using Harry.Data.DbLink;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data
{
    public static class RepositoryFactoryExtensions
    {
        public static IRepository CreateRepository(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName, params Type[] entityTypes)
        {
            Check.NotNull(dbLinkName, nameof(dbLinkName));

            Check.NotNull(serviceProvider, nameof(serviceProvider));

            var dbLink = serviceProvider.GetRequiredService<IDbLinkFactory>().GetDbLink(dbLinkName);

            if (dbLink == null) throw new Exception($"未找到名称为:{dbLinkName}的数据库链接配置.");

            return repositoryFactory.CreateRepository(serviceProvider, dbLink, entityTypes);
        }


        public static IRepository CreateRepository<TEntity>(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));
            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName, typeof(TEntity));
        }

        public static IRepository CreateRepository<TEntity1, TEntity2>(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName, typeof(TEntity1), typeof(TEntity2));
        }

        public static IRepository CreateRepository<TEntity1, TEntity2, TEntity3>(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                );
        }

        public static IRepository CreateRepository<
            TEntity1
            , TEntity2
            , TEntity3
            , TEntity4
            >(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                , typeof(TEntity4)
                );
        }

        public static IRepository CreateRepository<
            TEntity1
            , TEntity2
            , TEntity3
            , TEntity4
            , TEntity5
            >(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                , typeof(TEntity4)
                , typeof(TEntity5)
                );
        }

        public static IRepository CreateRepository<
            TEntity1
            , TEntity2
            , TEntity3
            , TEntity4
            , TEntity5
            , TEntity6
            >(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
            where TEntity6 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                , typeof(TEntity4)
                , typeof(TEntity5)
                , typeof(TEntity6)
                );
        }

        public static IRepository CreateRepository<
            TEntity1
            , TEntity2
            , TEntity3
            , TEntity4
            , TEntity5
            , TEntity6
            , TEntity7
            >(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
            where TEntity6 : class, new()
            where TEntity7 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                , typeof(TEntity4)
                , typeof(TEntity5)
                , typeof(TEntity6)
                , typeof(TEntity7)
                );
        }

        public static IRepository CreateRepository<
            TEntity1
            , TEntity2
            , TEntity3
            , TEntity4
            , TEntity5
            , TEntity6
            , TEntity7
            , TEntity8
            >(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
            where TEntity6 : class, new()
            where TEntity7 : class, new()
            where TEntity8 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                , typeof(TEntity4)
                , typeof(TEntity5)
                , typeof(TEntity6)
                , typeof(TEntity7)
                , typeof(TEntity8)
                );
        }

        public static IRepository CreateRepository<
            TEntity1
            , TEntity2
            , TEntity3
            , TEntity4
            , TEntity5
            , TEntity6
            , TEntity7
            , TEntity8
            , TEntity9
            >(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
            where TEntity6 : class, new()
            where TEntity7 : class, new()
            where TEntity8 : class, new()
            where TEntity9 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                , typeof(TEntity4)
                , typeof(TEntity5)
                , typeof(TEntity6)
                , typeof(TEntity7)
                , typeof(TEntity8)
                , typeof(TEntity9)
                );
        }

        public static IRepository CreateRepository<
            TEntity1
            , TEntity2
            , TEntity3
            , TEntity4
            , TEntity5
            , TEntity6
            , TEntity7
            , TEntity8
            , TEntity9
            , TEntity10
            >(this IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider, string dbLinkName)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
            where TEntity6 : class, new()
            where TEntity7 : class, new()
            where TEntity8 : class, new()
            where TEntity9 : class, new()
            where TEntity10 : class, new()
        {
            if (repositoryFactory == null) throw new ArgumentNullException(nameof(repositoryFactory));
            if (dbLinkName == null) throw new ArgumentNullException(nameof(dbLinkName));

            return repositoryFactory.CreateRepository(serviceProvider, dbLinkName
                , typeof(TEntity1)
                , typeof(TEntity2)
                , typeof(TEntity3)
                , typeof(TEntity4)
                , typeof(TEntity5)
                , typeof(TEntity6)
                , typeof(TEntity7)
                , typeof(TEntity8)
                , typeof(TEntity9)
                , typeof(TEntity10)
                );
        }
    }
}
