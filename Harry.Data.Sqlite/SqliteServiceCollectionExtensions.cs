using Harry.Data;
using Harry.Data.Sqlite;
using Harry.SqlBuilder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqliteServiceCollectionExtensions
    {
        public static IDataBuilder AddSqlite(this IDataBuilder builder, Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null, string dbType = null, IEnumerable<IExtSql> exts = null)
        {
            dbType = dbType ?? "Sqlite";
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbProvider>(new SqliteDbProvider(sqliteOptionsAction, dbType)));
            return builder;
        }
    }
}
