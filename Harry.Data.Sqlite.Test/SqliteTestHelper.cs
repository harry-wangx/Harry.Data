using Harry.Data.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Harry.Data.Sqlite.Test
{
    public class SqliteTestHelper : TestHelper
    {
        protected SqliteTestHelper()
        { }

        private static Lazy<SqliteTestHelper> instance = new Lazy<SqliteTestHelper>(() =>
        {
            var result = new SqliteTestHelper();
            return result;
        });

        public static SqliteTestHelper Instance { get; } = instance.Value;

        public override IDataBuilder AddProviderServices(IDataBuilder builder)
        {
            builder.AddSqlite(options =>
            {

            });
            //services.Configure<RepositoryOptions>(options =>
            //{
            //    options.DbLinks.Add(new DbLink.DbLinkItem() { Name = "test", DbType = "Sqlite", ConnectionString = "Data Source=test.db" });
            //});
            builder.AddDbLink("test", "Sqlite", "Data Source=test.db");
            return builder;
        }

        public override void ConfigDbContextOptions(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=test.db");
        }

    }
}
