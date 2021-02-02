using Harry.Data.Samples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harry.Data.Test
{
    public abstract class TestHelper
    {
        public abstract IDataBuilder AddProviderServices(IDataBuilder services);

        /// <summary>
        /// 配置初始化DbContext
        /// </summary>
        public abstract void ConfigDbContextOptions(DbContextOptionsBuilder options);

        public IServiceProvider CreateServiceProvider(IServiceCollection customServices = null)
        {
            var result = CreateServiceProvider(customServices, AddProviderServices);
            InitDatabase(result);
            return result;
        }

        private IServiceProvider CreateServiceProvider(
        IServiceCollection customServices,
        Func<IDataBuilder, IDataBuilder> addProviderServices)
        {
            var services = new ServiceCollection();
            
            if (customServices != null)
            {
                foreach (var service in customServices)
                {
                    services.Add(service);
                }
            }

            services.AddEFCoreRepository(builder =>
            {
                builder.Configure(options =>
                {
                    options.Register<UserModel>(e =>
                    {
                        e.ToTable("Users");
                        e.HasKey(m => m.Id);
                        e.Property(m => m.Id).ValueGeneratedNever();
                    });
                    options.Register<ArticleModel>(e =>
                    {
                        e.ToTable("Articles");
                        e.HasKey(m => m.Id);
                        e.Property(m => m.Id).ValueGeneratedNever();
                    });
                });

                addProviderServices(builder);


                //开发状态下可以打开sql语句监控
                //options.UseLogger();
            });

            services.AddDbContext<ApplicationDbContext>(ConfigDbContextOptions);

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitDatabase(IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

            //db.Database.EnsureDeleted();
            //if (db.Database.EnsureCreated())
            //{
            //    db.Users.AddRange(UserModel.User1, UserModel.User2);
            //    db.Articles.AddRange(ArticleModel.Article1, ArticleModel.Article2);
            //    db.SaveChanges();
            //}

            db.Database.EnsureCreated();
            db.Users.RemoveRange(db.Users.IgnoreQueryFilters());
            db.Articles.RemoveRange(db.Articles.IgnoreQueryFilters());
            db.SaveChanges();

            db.Users.AddRange(UserModel.User1, UserModel.User2);
            db.Articles.AddRange(ArticleModel.Article1, ArticleModel.Article2);
            db.SaveChanges();

        }
    }
}
