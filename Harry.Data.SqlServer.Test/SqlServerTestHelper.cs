using Harry.Data.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Harry.Data.SqlServer.Test
{
    public class SqlServerTestHelper : TestHelper
    {
        protected SqlServerTestHelper()
        { }

        private static Lazy<SqlServerTestHelper> instance = new Lazy<SqlServerTestHelper>(() =>
        {
            var result = new SqlServerTestHelper();
            return result;
        });

        public static SqlServerTestHelper Instance { get; } = instance.Value;

        public override IDataBuilder AddProviderServices(IDataBuilder builder)
        {
            builder.AddSqlServer(options =>
            {
            });
            //builder.Services.Configure<RepositoryOptions>(options =>
            //{
            //    options.DbLinks.Add(new DbLink.DbLinkItem() { Name = "test", DbType = "SqlServer", ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Harry.Data.SqlServer.Test;Trusted_Connection=True;MultipleActiveResultSets=true" });
            //});
            builder.AddDbLink("test", "SqlServer", "Server=(localdb)\\mssqllocaldb;Database=Harry.Data.SqlServer.Test;Trusted_Connection=True;MultipleActiveResultSets=true");
            return builder;
        }

        public override void ConfigDbContextOptions(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Harry.Data.SqlServer.Test;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    }
}
