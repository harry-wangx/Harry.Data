using System.Runtime.CompilerServices;
using Harry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harry.Data.DbLink;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


[assembly: InternalsVisibleTo("Harry.Data.Test")]

namespace Harry.Data
{
    public class RepositoryOptions
    {
        //private readonly Dictionary<Type, Type> dicEntityTypes = new Dictionary<Type, Type>();
        //private readonly Type dbType = typeof(DbContext);
        private readonly Dictionary<Type, Action<ModelBuilder>> _modelBuilderActions = new Dictionary<Type, Action<ModelBuilder>>();

        ///// <summary>
        ///// 自动注册DbContext与其所属DbSet对应的实体类型
        ///// </summary>
        ///// <typeparam name="TDbContext"></typeparam>
        ///// <returns></returns>
        //public RepositoryOptions Register<TDbContext>() where TDbContext : DbContext
        //{
        //    var dbContextType = typeof(TDbContext);
        //    //获取通过DbSet定义的实体信息集合
        //    var lstEntityTypes = dbContextType.GetProperties((BindingFlags.Public | BindingFlags.Instance))
        //        .Where(m => ReflectionHelper.IsAssignableToGenericType(m.PropertyType, typeof(DbSet<>))
        //        //&& ReflectionHelper.IsAssignableToGenericType(m.PropertyType.GetGenericArguments()[0], typeof(IEntity<>))
        //        ).Select(m => m.PropertyType.GetGenericArguments()[0]).ToList();

        //    lstEntityTypes.ForEach(m =>
        //    {
        //        dicEntityTypes.Add(m, dbContextType);
        //    });

        //    return this;
        //}

        ///// <summary>
        ///// 数据库链接配置集合
        ///// </summary>
        //public List<DbLinkItem> DbLinks { get; } = new List<DbLinkItem>();

        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// 添加模型配置委托
        /// </summary>
        /// <param name="builder"></param>
        public void Register<TEntity>(Action<EntityTypeBuilder<TEntity>> builder)
            where TEntity : class
        {
            Check.NotNull(builder, nameof(builder));

            Type entityType = typeof(TEntity);
            if (_modelBuilderActions.ContainsKey(entityType))
            {
                return;
                //throw new Exception($"数据模型{entityType.ToString()}不能重复配置.");
            }
            else
            {
                _modelBuilderActions.Add(entityType, modelBuilder =>
                {
                    var e = modelBuilder.Entity<TEntity>();
                    builder.Invoke(e);
                });
            }
        }

        /// <summary>
        /// 获取指定类型的ModelBuilder委托
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected internal Action<ModelBuilder> GetModelBuilderAction(Type entityType)
        {
            if (_modelBuilderActions.TryGetValue(entityType, out Action<ModelBuilder> action))
            {
                return action;
            }
            else
            {
                return null;
                //throw new Exception($"未找到模型为[{entityType?.ToString()}]的配置项");
            }
        }
    }
}
