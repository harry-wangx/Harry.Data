using Harry.Data;
using Harry.Data.EntityFrameworkCore.PomeloMySql;
using Harry.SqlBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MySqlServiceCollectionExtensions
    {
        public static IDataBuilder AddPomeloMySql(this IDataBuilder builder, ServerVersion serverVersion, Action<MySqlDbContextOptionsBuilder> optionsBuilderAction = null, string dbType = null, Func<IEnumerable<IExtSql>> extsFunc = null)
        {
            dbType = dbType ?? "MySql";
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbProvider>(new MySqlDbProvider(serverVersion, optionsBuilderAction, dbType, extsFunc?.Invoke())));
            return builder;
        }
    }
}
