using Harry.SqlBuilder;
using Harry.SqlBuilder.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Harry.Data.Sqlite
{
    public class SqliteDbProvider : IDbProvider
    {
        private Action<SqliteDbContextOptionsBuilder> _sqliteOptionsAction;

        public SqliteDbProvider(Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction, string dbType = null, IEnumerable<IExtSql> exts = null)
        {
            this._sqliteOptionsAction = sqliteOptionsAction;
            this.DbType = dbType ?? "Sqlite";
            SqlBuilder = new Harry.SqlBuilder.Sqlite.SqlBuilder(exts);
        }

        public string DbType { get; private set; }



        public void Configure(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlite(connectionString, _sqliteOptionsAction);
        }

        public ISqlBuilder SqlBuilder { get; private set; }
    }
}
