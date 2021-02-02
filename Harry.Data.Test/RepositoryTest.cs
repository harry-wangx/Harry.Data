using Harry.Data.Samples.Models;
using Harry.SqlBuilder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

//#if INMEMORY
//using CurrentTestHelper = Harry.Data.Test.InMemoryTestHelper;
//#endif
#if SQLITE
using CurrentTestHelper = Harry.Data.Sqlite.Test.SqliteTestHelper;
#endif
#if  SQLSERVER
using CurrentTestHelper = Harry.Data.SqlServer.Test.SqlServerTestHelper;
#endif

namespace Harry.Data.Test
{
    public class RepositoryTest
    {
        [Fact]
        public void GetAll()
        {
            IServiceProvider serviceProvider = CurrentTestHelper.Instance.CreateServiceProvider();
            var repository = serviceProvider.CreateRepository<UserModel>();

            var result = repository.GetAll<UserModel>().Count();

            Assert.Equal(2, result);
        }


        [Fact]
        public async Task InsertAsync()
        {
            IServiceProvider serviceProvider = CurrentTestHelper.Instance.CreateServiceProvider();
            var repository = serviceProvider.CreateRepository<UserModel>();

            var result = await repository.InsertAsync(new UserModel() { Id = 3, Name = "User3" });

            Assert.Equal(1, result);
        }


        [Fact]
        public async Task UpdateAsync()
        {
            IServiceProvider serviceProvider = CurrentTestHelper.Instance.CreateServiceProvider();
            var repository = serviceProvider.CreateRepository<UserModel>();

            var lst = repository.GetAll<UserModel>().ToList();

            var user2 = lst.First(m => m.Id == 2);
            user2.Age = 21;
            var user3 = new UserModel() { Id = 3, Age = 1 };

            var result2 = await repository.UpdateAsync(user2, c => c.Column("Name", "User2New").Where("Id=@id", new SqlBuilderParameter("id", user2.Id)));
            var result3 = await repository.UpdateAsync(user3, c => c.Column("Name", "User3New").Where("Id=@id", new SqlBuilderParameter("id", user3.Id)));

            var newUser = repository.GetAll<UserModel>().Where(m => m.Id == 2).First();

            //正常更新,返回更新记录数
            Assert.Equal(1, result2);
            //数据不存在,更新失败,返回0
            Assert.Equal(0, result3);
            //正常更新名称
            Assert.Equal("User2New", newUser.Name);
            //此时Age正常值为0,只有在Update方法中,手动更新的字段,者会更新到数据库
            Assert.Equal(0, newUser.Age);
            //更新字段数据时,同时更新最后修改时间
            Assert.Equal(user2.LastModificationTime?.ToString("yyyy-MM-dd HH:mm:ss"), newUser.LastModificationTime?.ToString("yyyy-MM-dd HH:mm:ss"));
        }


        [Fact]
        public async void DeleteAsync()
        {
            IServiceProvider serviceProvider = CurrentTestHelper.Instance.CreateServiceProvider();
            var repository = serviceProvider.CreateRepository<UserModel>();

            var lst = repository.GetAll<UserModel>().ToList();

            var user1 = new UserModel() { Id = 1 };
            var user2 = lst.First(m => m.Id == 2);
            var user3 = new UserModel() { Id = 3 };

            //默认使用软删除
            var result1 = await repository.DeleteAsync(user1, w => w.Where("Id=@id", new SqlBuilderParameter("id", user1.Id)));
            //不使用软删除
            var result2 = await repository.DeleteAsync(user2, w => w.Where("Id=@id", new SqlBuilderParameter("id", user2.Id)), false);
            var result3 = await repository.DeleteAsync(user3, w => w.Where("Id=@id", new SqlBuilderParameter("id", user3.Id)));
            lst = repository.GetAll<UserModel>(false).ToList();

            //正常删除,返回删除记录数
            Assert.Equal(1, result1);
            //正常删除,返回删除记录数
            Assert.Equal(1, result2);
            //数据不存在,删除失败,返回0
            Assert.Equal(0, result3);
            //使用软删除,实际数据还在
            Assert.Equal(1, lst.Count(m => m.Id == 1));
            //不用软删除,数据已从数据库删掉
            Assert.Equal(0, lst.Count(m => m.Id == 2));
            //软删除
            Assert.True(lst.First(m => m.Id == 1).IsDeleted);
            //使用软删除,设置删除时间
            Assert.Equal(user1.DeletionTime?.ToString("yyyy-MM-dd HH:mm:ss"), lst.First(m => m.Id == 1).DeletionTime?.ToString("yyyy-MM-dd HH:mm:ss"));

            Assert.Equal(0, repository.GetAll<UserModel>().Count());
            Assert.Equal(1, repository.GetAll<UserModel>(false).Count());
        }

#if SQLITE || SQLSERVER
        [Fact]
        public async Task Commit()
        {
            IServiceProvider serviceProvider = CurrentTestHelper.Instance.CreateServiceProvider();
            var repository = serviceProvider.CreateRepository<UserModel>();
            //repository.BeginTrans();
            using (var tran = repository.DbContext.Database.BeginTransaction())
            {
                var result = await repository.InsertAsync(new UserModel() { Id = 3, Name = "User3" });
                tran.Commit();

                Assert.Equal(1, result);
                Assert.Equal(3, repository.GetAll<UserModel>().Count());
            }
        }

        [Fact]
        public async Task Rollback()
        {
            IServiceProvider serviceProvider = CurrentTestHelper.Instance.CreateServiceProvider();
            var repository = serviceProvider.CreateRepository<UserModel>();
            using (var tran = repository.DbContext.Database.BeginTransaction())
            {

                var result = await repository.InsertAsync(new UserModel() { Id = 3, Name = "User3" });
                tran.Rollback();

                Assert.Equal(1, result);
                Assert.Equal(2, repository.GetAll<UserModel>().Count());
            }
        }
#endif
    }
}
