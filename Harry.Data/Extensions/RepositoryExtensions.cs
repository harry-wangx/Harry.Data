using Harry.Data.Entities.Auditing;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Harry.Data.Events;
using Microsoft.Extensions.Options;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Harry.SqlBuilder;
using Dapper;
using Harry.Data.Entities;
using System.Data;
using System.Text;
using Harry.EventBus;
using System.Threading;

namespace Harry.Data
{
    public static class RepositoryExtensions
    {
        public static DbConnection GetDbConnection(this IRepository repository)
        {
            return repository.DbContext.Database.GetDbConnection();
        }

        /// <summary>
        /// 插入操作前调用
        /// </summary>
        public static void OnInserting(this IRepository repository, object entity)
        {
            if (entity == null) return;

            foreach (var item in repository.Handlers)
            {
                item.OnInserting(entity);
            }
        }

        /// <summary>
        /// 更新操作前调用
        /// </summary>
        public static void OnUpdating(this IRepository repository, object entity)
        {
            if (entity == null) return;

            foreach (var item in repository.Handlers)
            {
                item.OnUpdating(entity);
            }
        }

        /// <summary>
        /// 删除操作前调用
        /// </summary>
        public static void OnDeleting(this IRepository repository, object entity)
        {
            if (entity == null) return;

            foreach (var item in repository.Handlers)
            {
                item.OnDeleting(entity);
            }
        }

        /// <summary>
        /// 获取实体元数据
        /// </summary>
        public static IEntityType GetEntityType(this IRepository repository, Type type)
        {
            return repository.DbContext.Model.FindEntityType(type);
        }

        /// <summary>
        /// 获取表名称和Schema
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, string> GetTableNameAndSchema(this IRepository repository, Type type)
        {
            var entityType = GetEntityType(repository, type);

            return new Tuple<string, string>(entityType?.GetTableName(), entityType?.GetSchema());

        }

        /// <summary>
        /// 获取完整表名,如[dbo].[Users]
        /// </summary>
        public static string GetFullTableName(this IRepository repository, Type type)
        {
            var mapping = repository.GetTableNameAndSchema(type);
            string tableName;
            if (!string.IsNullOrEmpty(mapping.Item2))
            {
                tableName = repository.DbProvider.SqlBuilder.SqlGenerationHelper.DelimitIdentifier(mapping.Item2);
                tableName += ".";
            }
            else
            {
                tableName = "";
            }
            tableName += repository.DbProvider.SqlBuilder.SqlGenerationHelper.DelimitIdentifier(mapping.Item1);
            return tableName;
        }

        /// <summary>
        /// 获取表名(纯表名,不带符号)
        /// </summary>
        public static string GetTableName(this IRepository repository, Type type)
        {
            return repository.GetTableNameAndSchema(type)?.Item1;
        }


        #region 查询
        /// <summary>
        /// 查询所有数据(无数据追踪)
        /// </summary>
        /// <param name="useSoftDelete"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetAll<TEntity>(this IRepository repository, bool useSoftDelete = true)
            where TEntity : class
        {
            IQueryable<TEntity> results = repository.DbContext.Set<TEntity>();
            if (useSoftDelete)
                return results.AsNoTracking();
            else
                return results.IgnoreQueryFilters().AsNoTracking();
        }

        #endregion

        #region 插入/更改/删除
        public static int Insert<TEntity>(this IRepository repository, TEntity entity)
            where TEntity : class
        {
            repository.OnInserting(entity);
            repository.DbContext.Set<TEntity>().Add(entity);
            return repository.DbContext.SaveChanges();
        }

        public static Task<int> InsertAsync<TEntity>(this IRepository repository, TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            repository.OnInserting(entity);
            repository.DbContext.Set<TEntity>().Add(entity);
            return repository.DbContext.SaveChangesAsync(cancellationToken);
        }

        //下面的方法不再使用,更新操作的时候,还是显示指定更新哪些字段好
        //public static void AttachIfNot<TEntity>(this IRepository repository, TEntity entity)
        //    where TEntity : class
        //{
        //    var entry = repository.DbContext.ChangeTracker.Entries().FirstOrDefault(ent => entity.Equals(ent.Entity));
        //    if (entry != null)
        //    {
        //        if (ReferenceEquals(entity, entry.Entity))
        //        {
        //            //引用相同,直接返回
        //            return;
        //        }
        //        else
        //        {
        //            //引用不同,但是ID相同,移除原跟踪
        //            entry.State = EntityState.Detached;
        //        }
        //    }
        //    repository.DbContext.Set<TEntity>().Attach(entity);
        //}

        public static Task<int> UpdateAsync<TEntity>(this IRepository repository, TEntity entity, Action<IUpdateBuilder> action=null)
            where TEntity : class
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var mapping = GetTableNameAndSchema(repository, typeof(TEntity));

            var builder = repository.DbProvider.SqlBuilder.Update(mapping.Item1, mapping.Item2);

            action?.Invoke(builder);

            OnUpdating(repository, entity);

            //
            autosetUpdate(entity, builder);

            var cmd = builder.ToCommand();
            return repository.GetDbConnection().ExecuteAsync(cmd);
        }

        private static void autosetUpdate<TEntity>(TEntity entity, IUpdateBuilder builder) where TEntity : class
        {
            //有并发控制
            if (entity != null && entity is IHasConcurrencyStamp concurrencyStampEntity && !string.IsNullOrEmpty(concurrencyStampEntity.ConcurrencyStamp))
            {
                builder.Column("ConcurrencyStamp", Guid.NewGuid().ToString());
                builder.Where("ConcurrencyStamp=@OldConcurrencyStamp", new SqlBuilderParameter("OldConcurrencyStamp", concurrencyStampEntity.ConcurrencyStamp));
            }

            var type = typeof(TEntity);

            if (typeof(IHasModificationTime).IsAssignableFrom(type))
            {
                builder.Column("LastModificationTime", (entity as IHasModificationTime)?.LastModificationTime ?? DateTime.Now);
            }
            else
            {
                return;
            }

            if (entity != null && type.GetInterfaces().Any(m => m.IsGenericType && m.GetGenericTypeDefinition() == typeof(IModificationAudited<>)))
            {
                builder.Column("LastModifierId", entity != null ? type.GetProperty("LastModifierId").GetValue(entity, null) : 0);
                builder.Column("LastModifierName", entity != null ? (type.GetProperty("LastModifierName").GetValue(entity, null) ?? string.Empty) : string.Empty);
            }
        }

        public static Task<int> UpdateAsync<TEntity>(this IRepository repository, Action<IUpdateBuilder> action=null)
            where TEntity : class, new()
        {
            return UpdateAsync(repository, new TEntity(), action);
        }


        public static Task<int> DeleteAsync<TEntity>(this IRepository repository, TEntity entity, Action<IWhere> action, bool useSoftDelete = true) where TEntity : class
        {
            var mapping = GetTableNameAndSchema(repository, typeof(TEntity));
            SqlBuilderCommand cmd;
            //使用软删除
            if (useSoftDelete && typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var builder = repository.DbProvider.SqlBuilder.Update(mapping.Item1, mapping.Item2);

                //设置删除时间
                OnDeleting(repository, entity);

                //有并发控制
                if (entity != null && entity is IHasConcurrencyStamp concurrencyStampEntity && !string.IsNullOrEmpty(concurrencyStampEntity.ConcurrencyStamp))
                {
                    builder.Column("ConcurrencyStamp", Guid.NewGuid().ToString());
                    builder.Where("ConcurrencyStamp=@OldConcurrencyStamp", new SqlBuilderParameter("OldConcurrencyStamp", concurrencyStampEntity.ConcurrencyStamp));
                }

                var type = typeof(TEntity);

                if (typeof(IHasDeletionTime).IsAssignableFrom(type))
                {
                    builder.Column("DeletionTime", (entity as IHasDeletionTime)?.DeletionTime ?? DateTime.Now);


                    if (type.GetInterfaces().Any(m => m.IsGenericType && m.GetGenericTypeDefinition() == typeof(IDeletionAudited<>)))
                    {
                        builder.Column("DeleterId", entity != null ? type.GetProperty("DeleterId").GetValue(entity, null) : 0);
                        builder.Column("DeleterName", entity != null ? (type.GetProperty("DeleterName").GetValue(entity, null) ?? string.Empty) : string.Empty);
                    }
                }

                builder.Column("IsDeleted", true, DbType.Boolean);
                action?.Invoke(builder);
                cmd = builder.ToCommand();
            }
            else
            {
                //直接删除
                var builder = repository.DbProvider.SqlBuilder.Delete(mapping.Item1, mapping.Item2);
                action?.Invoke(builder);
                cmd = builder.ToCommand();
            }

            return repository.GetDbConnection().ExecuteAsync(cmd);
        }

        public static Task<int> DeleteAsync<TEntity>(this IRepository repository, Action<IWhere> action, bool useSoftDelete = true)
            where TEntity : class, new()
        {
            return DeleteAsync<TEntity>(repository, new TEntity(), action, useSoftDelete);
        }

        public static Task<int> DeleteByIdAsync<TEntity>(this IRepository repository, TEntity entity, object id, string idName = null, bool useSoftDelete = true)
            where TEntity : class
        {
            Check.NotNull(repository, nameof(repository));
            Check.NotNull(repository, nameof(id));
            if (string.IsNullOrEmpty(idName))
            {
                idName = "Id";
            }

            return DeleteAsync<TEntity>(repository, entity, w => w.Where($"{repository.DbProvider.SqlBuilder.SqlGenerationHelper.DelimitIdentifier(idName)}={repository.DbProvider.SqlBuilder.SqlGenerationHelper.GenerateParameterName("id")}"
                , new SqlBuilderParameter("id", id)), useSoftDelete);
        }

        public static Task<int> DeleteByIdAsync<TEntity>(this IRepository repository, object id, string idName = null, bool useSoftDelete = true)
            where TEntity : class, new()
        {
            return DeleteByIdAsync<TEntity>(repository, new TEntity(), id, idName, useSoftDelete);
        }
        #endregion



        /// <summary>
        /// 判断指定条件的数据是否存在
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate">限定条件</param>
        /// <returns></returns>
        public static bool Exists<TEntity>(this IRepository repository, Expression<Func<TEntity, bool>> predicate, bool useSoftDelete = true) where TEntity : class
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return repository.GetAll<TEntity>(useSoftDelete)
                .Where(predicate).Count() > 0;
        }

        /// <summary>
        ///判断指定编号的数据是否存在
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="id">编号</param>
        /// <param name="idName">编号字段名称，默认为"Id"</param>
        /// <returns></returns>
        public static bool ExistsById<TEntity, TPrimaryKey>(this IRepository repository, TPrimaryKey id, string idName = null) where TEntity : class
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            return repository.GetAll<TEntity>(false)
                .Where(DataHelper.CreateEqualityExpressionForId<TEntity, TPrimaryKey>(id, idName))
                .Count() > 0;
        }

        public static Task<int> SetEnabled<TEntity>(this IRepository repository, object id, bool enabled, string idName = null)
            where TEntity : class, IEnabled
        {
            Check.NotNull(repository, nameof(repository));
            Check.NotNull(repository, nameof(id));

            if (string.IsNullOrEmpty(idName))
            {
                idName = "Id";
            }

            string tableName = GetFullTableName(repository, typeof(TEntity));

            StringBuilder sb = new StringBuilder("UPDATE ");
            sb.Append(tableName);
            sb.Append(" SET ");
            repository.DbProvider.SqlBuilder.SqlGenerationHelper.DelimitIdentifier(sb, "IsEnabled");
            sb.Append("=");
            repository.DbProvider.SqlBuilder.SqlGenerationHelper.GenerateParameterName(sb, "IsEnabled");
            sb.Append(" WHERE ");
            repository.DbProvider.SqlBuilder.SqlGenerationHelper.DelimitIdentifier(sb, idName);
            sb.Append("=");
            repository.DbProvider.SqlBuilder.SqlGenerationHelper.GenerateParameterName(sb, "id");

            var cmd = repository.DbProvider.SqlBuilder.Raw()
                .Append(sb.ToString(), new SqlBuilderParameter("IsEnabled", enabled, DbType.Boolean), new SqlBuilderParameter("id", id))
                .ToCommand();
            return repository.GetDbConnection().ExecuteAsync(cmd);

        }

        #region 事件相关
        /// <summary>
        /// 发布数据变更事件
        /// </summary>
        /// <returns></returns>
        public static IRepository Publish(this IRepository repository, DataChangeEvent @event, string eventName = null, string eventBusName = null)
        {
            var bus = repository.ServiceProvider.GetRequiredService<IEventBusFactory>().CreateEventBus(eventBusName);
            bus.Publish(@event, eventName);
            return repository;
        }

        /// <summary>
        /// 发布数据变更事件
        /// </summary>
        /// <returns></returns>
        public static IRepository Publish<T>(this IRepository repository, string id, EventType eventType)
            where T : class
        {
            return repository.Publish(new DataChangeEvent()
            {
                DataType = typeof(T).GetFullName(),
                DataId = id,
                EventType = eventType
            });
        }

        /// <summary>
        /// 发布数据变更事件
        /// </summary>
        /// <returns></returns>
        public static IRepository Publish(this IRepository repository, string dataType, string id, EventType eventType)
        {
            return repository.Publish(new DataChangeEvent()
            {
                DataType = dataType,
                DataId = id,
                EventType = eventType
            });
        }

        #endregion
    }
}
