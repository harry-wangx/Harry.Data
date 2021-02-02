using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Harry.SqlBuilder;
using Harry.SqlBuilder.MySql;

namespace Harry.Data.EntityFrameworkCore.PomeloMySql
{
    public class MySqlDbProvider : IDbProvider
    {
        private Action<MySqlDbContextOptionsBuilder> _optionsBuilderAction;
        private ServerVersion _serverVersion;

        public MySqlDbProvider(ServerVersion serverVersion, Action<MySqlDbContextOptionsBuilder> optionsBuilderAction, string dbType = null, IEnumerable<IExtSql> exts = null)
        {
            this._serverVersion = serverVersion ?? throw new ArgumentNullException(nameof(serverVersion));
            this._optionsBuilderAction = optionsBuilderAction;
            this.DbType = dbType ?? "MySql";
            SqlBuilder = new Harry.SqlBuilder.MySql.SqlBuilder(exts);
        }

        public string DbType { get; private set; }

        public void Configure(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseMySql(connectionString, _serverVersion, _optionsBuilderAction);
        }

        public ISqlBuilder SqlBuilder { get; private set; }
    }
}
