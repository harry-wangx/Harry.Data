using Harry.SqlBuilder;
using Microsoft.EntityFrameworkCore;
using System;

namespace Harry.Data
{
    public interface IDbProvider
    {
        string DbType { get; }

        void Configure(DbContextOptionsBuilder builder, string connectionStringOrDatabaseName);


        ISqlBuilder SqlBuilder { get; }
    }
}
