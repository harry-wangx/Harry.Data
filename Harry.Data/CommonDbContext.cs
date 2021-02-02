using Harry.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;


namespace Harry.Data
{
    public class CommonDbContext : DbContext
    {
        private IDbProvider dbProvider;
        private string connectionString;

        private readonly RepositoryOptions _options;
        private readonly Type[] _entityTypes;

        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        protected CommonDbContext(IOptions<RepositoryOptions> optionsAccessor, Type[] entityTypes) :
            this(optionsAccessor)
        {
            this._entityTypes = entityTypes ?? throw new ArgumentNullException(nameof(entityTypes));
        }

        public void Init(IDbProvider dbProvider, string connectionString)
        {
            this.dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (_entityTypes != null && _entityTypes.Length > 0)
            {
                foreach (var item in _entityTypes)
                {
                    _options.GetModelBuilderAction(item)?.Invoke(modelBuilder);
                    if (typeof(ISoftDelete).IsAssignableFrom(item))
                    {
                        //使用软删除
                        var lambdaParam = Expression.Parameter(item);

                        var lambdaBody = Expression.Equal(
                            Expression.PropertyOrField(lambdaParam, "IsDeleted"),
                            Expression.Constant(false, typeof(bool))
                            );
                        modelBuilder.Entity(item).HasQueryFilter(Expression.Lambda(lambdaBody, lambdaParam));
                    }
                }
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options.LoggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(_options.LoggerFactory);
            }
            base.OnConfiguring(optionsBuilder);

            dbProvider.Configure(optionsBuilder, connectionString);
        }
    }

    public sealed class CommonDbContext<T> : CommonDbContext
        where T : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] { typeof(T) })
        { }
    }

    public sealed class CommonDbContext<T1, T2> : CommonDbContext
        where T1 : class
        where T2 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3, T4> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
                , typeof(T4)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3, T4, T5> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
                , typeof(T4)
                , typeof(T5)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3, T4, T5, T6> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
                , typeof(T4)
                , typeof(T5)
                , typeof(T6)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3, T4, T5, T6, T7> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
                , typeof(T4)
                , typeof(T5)
                , typeof(T6)
                , typeof(T7)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3, T4, T5, T6, T7, T8> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
                , typeof(T4)
                , typeof(T5)
                , typeof(T6)
                , typeof(T7)
                , typeof(T8)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3, T4, T5, T6, T7, T8, T9> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
                , typeof(T4)
                , typeof(T5)
                , typeof(T6)
                , typeof(T7)
                , typeof(T8)
                , typeof(T9)
            })
        { }
    }

    public sealed class CommonDbContext<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : CommonDbContext
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
    {
        public CommonDbContext(IOptions<RepositoryOptions> optionsAccessor)
            : base(optionsAccessor, new Type[] {
                typeof(T1)
                , typeof(T2)
                , typeof(T3)
                , typeof(T4)
                , typeof(T5)
                , typeof(T6)
                , typeof(T7)
                , typeof(T8)
                , typeof(T9)
                , typeof(T10)
            })
        { }
    }
}
