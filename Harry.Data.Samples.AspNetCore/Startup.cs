using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harry.Data.Samples.AspNetCore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Harry.Data.Samples.Models;
using Harry.Data.DbLink;
using Microsoft.Extensions.Logging;
using Harry.Data.Log;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Harry.SqlBuilder;
using Microsoft.Extensions.Hosting;

namespace Harry.Data.Samples.AspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEFCoreRepository(builder =>
            {
                //options.DbLinks.Add(new DbLinkItem() { Name = "TestSqlite", DbType = "Sqlite", ConnectionString = "Data Source=test.db" });
                builder.AddDbLink("TestSqlite", "Sqlite", "Data Source=test.db");

                builder.Configure(options =>
                {
                    options.Register<UserModel>(e =>
                    {
                        e.ToTable("Users");
                        e.HasKey(m => m.Id);
                    });
                    options.Register<ArticleModel>(e =>
                    {
                        e.ToTable("Articles");
                        e.HasKey(m => m.Id);
                    });

                    //开发状态下可以打开sql语句监控
                    options.UseLogger();
                })
                .AddWebUserHandler(value => int.TryParse(value, out int id) ? id : 0)
                .AddSqlite();
            });


            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                //context.User.Identity
                //await context.SignInAsync(IdentityConstants.ApplicationScheme
                //    , new ClaimsPrincipal(new Identity(
                //    new AuthenticationProperties());

                context.Response.ContentType = "text/plain;charset=utf-8";
                //初始化数据库
                var entity = new UserModel() { Name = "Admin" + DateTime.Now.ToString("yyyyMMddHHmmss") };

                var repository = context.RequestServices.GetRequiredService<IRepositoryFactory>().CreateRepository<UserModel>(context.RequestServices, "TestSqlite");

                //插入
                var insertCount = await repository.InsertAsync(entity);

                //更新
                var updateCount = await repository.UpdateAsync(entity, builder => builder
                     .Column("Name", entity.Name + ":名称已修改")
                     .Where("Id=@id", new SqlBuilderParameter("id", entity.Id))
                );

                //删除
                var deleteCount = await repository.DeleteAsync(entity, w => w.Where("Id=@id", new SqlBuilderParameter("id", entity.Id)), true);

                var repositoryArticle = context.RequestServices.GetRequiredService<IRepositoryFactory>().CreateRepository<ArticleModel>(context.RequestServices, "TestSqlite");
                await repositoryArticle.InsertAsync(new ArticleModel() { Title = "文章标题:" + DateTime.Now.ToString() });

                var list = repository.GetAll<UserModel>(false).OrderBy("id").ToList();

                //await context.Response.WriteAsync($"插入:{insertCount},更新:{updateCount},删除:{deleteCount},共计:{repository.GetAll<UserModel>().Count()},忽略软删除后数量:{repository.GetAll<UserModel>(false).Count()}", System.Text.Encoding.UTF8);
            });
        }
    }
}
