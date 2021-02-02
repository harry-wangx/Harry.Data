using Harry.Data;
using Harry.Data.PostgreSQL;
using Harry.SqlBuilder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IDataBuilder AddPostgreSQL(this IDataBuilder builder, Action<NpgsqlDbContextOptionsBuilder> npgsqlOptionsAction = null, string dbType = null, IEnumerable<IExtSql> exts = null)
        {
            dbType = dbType ?? "PostgreSQL";
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbProvider>(new DbProvider(npgsqlOptionsAction, dbType, exts)));
            return builder;
        }
    }
}
