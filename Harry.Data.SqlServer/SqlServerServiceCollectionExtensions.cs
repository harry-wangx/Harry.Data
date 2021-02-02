using Harry.Data;
using Harry.Data.SqlServer;
using Harry.SqlBuilder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerServiceCollectionExtensions
    {
        public static IDataBuilder AddSqlServer(this IDataBuilder builder, Action<SqlServerDbContextOptionsBuilder> optionsAction = null, string dbType = null, IEnumerable<IExtSql> exts = null)
        {
            dbType = dbType ?? "SqlServer";
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbProvider>(new SqlServerDbProvider(optionsAction, dbType)));
            return builder;
        }
    }
}
