using Harry.SqlBuilder;
using Harry.SqlBuilder.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Harry.Data.SqlServer
{
    public class SqlServerDbProvider : IDbProvider
    {
        private Action<SqlServerDbContextOptionsBuilder> _optionsAction;

        public SqlServerDbProvider(Action<SqlServerDbContextOptionsBuilder> optionsAction, string dbType = null, IEnumerable<IExtSql> exts = null)
        {
            this._optionsAction = optionsAction;
            this.DbType = dbType ?? "SqlServer";
            SqlBuilder = new Harry.SqlBuilder.SqlServer.SqlBuilder(exts);
        }

        public string DbType { get; private set; }

        public void Configure(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString, _optionsAction);
        }

        public ISqlBuilder SqlBuilder { get; private set; }
    }
}
