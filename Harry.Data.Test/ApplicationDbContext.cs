using Harry.Common;
using Harry.Data.Entities;
using Harry.Data.Samples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Harry.Data.Test
{

    public class ApplicationDbContext : DbContext
    {
        private readonly RepositoryOptions _options;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<RepositoryOptions> optionsAccessor)
            : base(options)
        {
            _options = optionsAccessor.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var lstEntityTypes = this.GetType().GetProperties((BindingFlags.Public | BindingFlags.Instance))
                .Where(m => ReflectionHelper.IsAssignableToGenericType(m.PropertyType, typeof(DbSet<>))
                //&& ReflectionHelper.IsAssignableToGenericType(m.PropertyType.GetGenericArguments()[0], typeof(IEntity<>))
                ).Select(m => m.PropertyType.GetGenericArguments()[0]).ToList();

            foreach (var item in lstEntityTypes)
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

        public DbSet<UserModel> Users { get; set; }

        public DbSet<ArticleModel> Articles { get; set; }
    }
}
