using Harry.SqlBuilder;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;

namespace Harry.Data.PostgreSQL
{
    public class DbProvider : IDbProvider
    {
        private Action<NpgsqlDbContextOptionsBuilder> npgsqlOptionsAction = null;

        public DbProvider(Action<NpgsqlDbContextOptionsBuilder> npgsqlOptionsAction, string dbType = null, IEnumerable<IExtSql> exts = null)
        {
            this.npgsqlOptionsAction = npgsqlOptionsAction;
            this.DbType = dbType ?? "PostgreSQL";
            //SqlBuilder = new Harry.SqlBuilder.Sqlite.SqlBuilder(exts);
        }

        public string DbType { get; private set; }



        public void Configure(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseNpgsql(connectionString, npgsqlOptionsAction);
        }

        public ISqlBuilder SqlBuilder { get; private set; }
    }
}
