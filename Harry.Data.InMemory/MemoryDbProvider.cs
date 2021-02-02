using Harry.SqlBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Harry.Data.InMemory
{
    public class MemoryDbProvider : IDbProvider
    {
        private Action<InMemoryDbContextOptionsBuilder> _optionsAction;
        private InMemoryDatabaseRoot databaseRoot;

        public MemoryDbProvider(Action<InMemoryDbContextOptionsBuilder> optionsAction = null, string dbType = null, InMemoryDatabaseRoot databaseRoot = null)
        {
            this._optionsAction = optionsAction;
            this.DbType = dbType ?? "InMemory";
            this.databaseRoot = databaseRoot;
        }

        public string DbType { get; private set; }

        public ISqlBuilderFactory SqlBuilder => throw new NotImplementedException();

        public void Configure(DbContextOptionsBuilder builder, string connectionStringOrDatabaseName)
        {
            builder.UseInMemoryDatabase(connectionStringOrDatabaseName, databaseRoot, _optionsAction);
        }
    }
}
