using Harry.Data.Samples.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Xunit;
using YesSql;
using YesSql.Provider.Sqlite;
using YesSql.Sql;

namespace Harry.Data.Sqlite.Test
{
    public class YesSqlTest
    {
        [Fact]
        public void Save()
        {
            using (var store = CreateStore())
            using (var session = store.CreateSession())
            {
                try
                {
                    var schemaBuilder = new SchemaBuilder(store.Configuration, session.DemandAsync().GetAwaiter().GetResult());
                    schemaBuilder.CreateMapIndexTable(nameof(UserIndex), table => table
                        .Column<string>("Name")
                    );
                }
                catch { }

                session.Save(new UserModel()
                {
                    Name = "Admin"
                });

                session.CommitAsync().GetAwaiter().GetResult();
            }
        }

        private static IStore CreateStore()
        {
            IConfiguration storeConfiguration = new YesSql.Configuration();


            var databaseFile = Path.Combine("yessql.db");
            storeConfiguration
                .UseSqLite($"Data Source={databaseFile};Cache=Shared", IsolationLevel.ReadUncommitted)
                .UseDefaultIdGenerator();

            var store = StoreFactory.CreateAsync(storeConfiguration).GetAwaiter().GetResult();

            store.RegisterIndexes(new UserIndexProvider());

            return store;
        }
    }
}
